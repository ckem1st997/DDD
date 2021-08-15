using GrpcProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private readonly IHttpClientFactory _clientFactory;
        private readonly ProductGrpc.ProductGrpcClient _client;
        private readonly IUserRepositories _userRepositories;
        private readonly IUsersIntegrationEventService _usersIntegrationEventService;
        public UsersController(IHttpClientFactory clientFactory,ProductGrpc.ProductGrpcClient client,IUserRepositories userRepositories, IUsersIntegrationEventService usersIntegrationEventService)
        {
            _userRepositories = userRepositories;
            _usersIntegrationEventService = usersIntegrationEventService;
            _client = client;
            _clientFactory = clientFactory;

        }


        [HttpPost("Addgprc")]
        public async Task<IActionResult> Add(GrpcCreateProductsCommand users)
        {
            if (users is null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            var mode = await _client.CreateProductDraftFromUserAsync(users);
            return Ok(mode);
        }

        [HttpPost("Editgprc")]
        public async Task<IActionResult> Edit(GrpcUpdateProductsCommand users)
        {
            if (users is null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            var mode = await _client.UpdateProductDraftFromUserAsync(users);
            return Ok(mode);
        }

        [HttpPost("Deletegprc")]
        public async Task<IActionResult> Delelte(GrpcDeleteProductsCommand users)
        {
            if (users is null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            var mode = await _client.DeleteProductDraftFromUserAsync(users);
            return Ok(mode);
        }



        [HttpPost("Listgprc")]
        public async Task<IActionResult> List(GrpcPaginatedListCommand users)
        {
            if (users is null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            var mode = await _client.PaginatedListDraftFromUserAsync(users);
            return Ok(mode);
        }
        









        [HttpPost("Add")]
        public async Task<IActionResult> Add(Users users)
        {
            var model = await _userRepositories.Expression(x => x.Username.Equals(users.Username));
            if (model.Count() > 0)
                return Ok(false);
            users.Roleu = "User";
            return Ok(await _userRepositories.AddAsync(users));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepositories.ListAllAsync());
        }

        [HttpGet("GetFirst")]
        public async Task<IActionResult> GetFirst(int id)
        {
            return Ok(await _userRepositories.GetFirstAsyncAsNoTracking(id));
        }


        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Users users)
        {
            users.Roleu = "User";
            return Ok(await _userRepositories.UpdateAsync(users));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _userRepositories.GetFirstAsyncAsNoTracking(id);

            if (id < 1 || model == null)
                return Ok(false);

            return Ok(await _userRepositories.DeleteAsync(model));
        }

        [HttpPost("PaginatedList")]
        public async Task<IActionResult> PaginatedList(int index, int number, string key)
        {
            if (index < 1 || number < 1)
                return Ok(false);

            return Ok(await _userRepositories.PaginatedList(index, number, key ?? ""));
        }









    }
}
