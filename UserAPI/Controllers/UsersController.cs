using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;
using UserAPI.IntegrationEvents;
using UserAPI.Service.UserEntity;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepositories _userRepositories;
        private readonly IUsersIntegrationEventService _usersIntegrationEventService;
        public UsersController(IUserRepositories userRepositories, IUsersIntegrationEventService usersIntegrationEventService)
        {
            _userRepositories = userRepositories;
            _usersIntegrationEventService = usersIntegrationEventService;
        }


        [HttpPost]
        public async Task<IActionResult> Add(Users users)
        {
            users.Roleu = "User";
            return Ok(await _userRepositories.AddAsync(users));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepositories.ListAllAsync());
        }









    }
}
