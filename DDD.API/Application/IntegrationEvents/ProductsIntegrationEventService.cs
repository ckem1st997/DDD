using DDD.Infrastructure.Context;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.IntegrationEvents
{

    // dùng để ghi lại quá trình thực hiện public lên RabbitMQ vào db
    public class ProductsIntegrationEventService : IProductsIntegrationEventService, IDisposable
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ProductsContext _userContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<ProductsIntegrationEventService> _logger;
        private volatile bool disposedValue;

        public ProductsIntegrationEventService(
            ILogger<ProductsIntegrationEventService> logger,
            IEventBus eventBus,
           ProductsContext userContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_userContext.Database.GetDbConnection());
        }


        // tiến hành public và cập nhật trạng thái vào db
        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from UserAPI - ({@IntegrationEvent})", evt.Id, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from UserAPI - ({@IntegrationEvent})", evt.Id, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }


        // lưu trữ dữ liệu public to RabbitMQ vào trong cơ sở dữ liệu
        public async Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            // Sử dụng chiến lược khả năng phục hồi của EF Core khi sử dụng nhiều DbContexts trong một BeginTransaction () rõ ràng:            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_userContext).ExecuteAsync(async () =>
            {
                // Đạt được tính nguyên tử giữa hoạt động cơ sở dữ liệu danh mục gốc và IntegrationEventLog nhờ một giao dịch cục bộ                await _userContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _userContext.Database.CurrentTransaction);
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (_eventLogService as IDisposable)?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
