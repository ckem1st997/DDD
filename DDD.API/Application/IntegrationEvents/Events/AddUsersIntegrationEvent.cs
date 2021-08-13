using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.IntegrationEvents.Events
{
    public record AddUsersIntegrationEvent : IntegrationEvent
    {

        public string Username { get; set; }

        public string Pass { get; set; }

        public string Roleu { get; set; }

        public bool Active { get; set; }

    }
}
