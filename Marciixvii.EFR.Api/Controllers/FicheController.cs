using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FicheController : ControllerBase {
        private readonly IFicheService _ficheService;

        public FicheController(IFicheService clientService) {
            _ficheService = clientService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<Fiche>>> GetAll() => Ok(await _ficheService.GetAll());

        [HttpGet("getbyid")]
        public async Task<ActionResult<Fiche>> GetById(int id) {
            Fiche client = await _ficheService.GetById(id);
            if(client == null) {
                return NotFound("Mawa trop");
            }

            return client;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Fiche>> Create(Fiche client) => await _ficheService.Create(client);

        [HttpPut("update")]
        public async Task<ActionResult<Fiche>> Update(Fiche client) => await _ficheService.Update(client);

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) => await _ficheService.Delete(id);

    }
}
