using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Helpers;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : ControllerBase {
        private readonly ILogger<UtilisateurController> _logger;
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(ILogger<UtilisateurController> logger, IUtilisateurService utilisateurService) {
            _logger = logger;
            _utilisateurService = utilisateurService;
        }

        [HttpGet(ApiRoute.CrudUrl.GetAll)]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAll() => await _utilisateurService.GetAll();

        [HttpGet(ApiRoute.CrudUrl.GetById)]
        public async Task<ActionResult<Utilisateur>> GetById(int id) {
            Utilisateur utilisateur = await _utilisateurService.GetById(id);
            if(utilisateur == null) {
                return NotFound();
            }

            return utilisateur;
        }

        [HttpPost(ApiRoute.CrudUrl.Create)]
        public async Task<ActionResult<Utilisateur>> Create(Utilisateur utilisateur) {
            Utilisateur utilisateur1 = await _utilisateurService.Create(utilisateur);
            if(utilisateur1 == null) {
                return BadRequest();
            }
            return Created("", utilisateur1);
        }

        [HttpPut(ApiRoute.CrudUrl.Update)]
        public async Task<ActionResult<bool>> Update(Utilisateur utilisateur) => await _utilisateurService.Update(utilisateur);

        [HttpDelete(ApiRoute.CrudUrl.Delete)]
        public async Task<ActionResult<bool>> Delete(int id) {
            bool found = await _utilisateurService.Delete(id);

            if(!found) {
                return NotFound();
            }

            return found;
        }

        [HttpGet(ApiRoute.UtilisateurUrl.Login)]
        public async Task<ActionResult<Utilisateur>> Login(string username, string password) {
            Utilisateur utilisateur = await _utilisateurService.Login(username, password);
            if(utilisateur == null) {
                return NotFound();
            }

            return utilisateur;
        }
    }
}
