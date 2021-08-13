using EventBus.Events;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task<int> SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        Task<int> MarkEventAsPublishedAsync(Guid eventId);
        Task<int> MarkEventAsInProgressAsync(Guid eventId);
        Task<int> MarkEventAsFailedAsync(Guid eventId);
    }
}
