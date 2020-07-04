using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase {
        private readonly IClientService ClientService;

        public ClientController(IClientService clientService) {
            ClientService = clientService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<Client>>> GetAll() => Ok(await ClientService.GetAll());

        [HttpGet("getbyid")]
        public async Task<ActionResult<Client>> GetById(int id) {
            Client client = await ClientService.GetById(id);
            if(client == null) {
                return NotFound("Mawa trop");
            }

            return client;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Client>> Create(Client client) => await ClientService.Create(client);

        [HttpPut("update")]
        public async Task<ActionResult<Client>> Update(Client client) => await ClientService.Update(client);

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) => await ClientService.Delete(id);

    }
}
