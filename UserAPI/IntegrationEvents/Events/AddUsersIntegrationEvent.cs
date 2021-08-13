using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.IntegrationEvents.Events
{

    // tạo sự kiện trùng với bên gửi để nhận dữ liệu
    // phải đăng ký
    public record AddUsersIntegrationEvent : IntegrationEvent
    {

        public string Username { get; set; }

        public string Pass { get; set; }

        public string Roleu { get; set; }

        public bool Active { get; set; }

    }
}
