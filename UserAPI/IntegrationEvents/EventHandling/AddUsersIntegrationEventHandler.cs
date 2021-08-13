using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;
using UserAPI.IntegrationEvents.Events;
using UserAPI.Service.UserEntity;

namespace UserAPI.IntegrationEvents.EventHandling
{

    // tạo sự kiện xử lý sự kiện nhận được
    // đăng ký
    public class AddUsersIntegrationEventHandler : IIntegrationEventHandler<AddUsersIntegrationEvent>
    {
        private readonly IUserRepositories _repository;
        private readonly ILogger<AddUsersIntegrationEventHandler> _logger;
        public AddUsersIntegrationEventHandler(IUserRepositories repository, ILogger<AddUsersIntegrationEventHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AddUsersIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

                var user = new Users();
                user.Username = @event.Username;
                user.Pass = @event.Username;
                user.Active = @event.Active;
                user.Roleu = "User";
                await _repository.AddAsync(user);
            }
        }
    }
}