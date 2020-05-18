using Marciixvii.EFR.App.Contracts;
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

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAll() => await _utilisateurService.GetAll();

        [HttpGet("getbyid")]
        public async Task<ActionResult<Utilisateur>> GetById(int id) {
            Utilisateur utilisateur = await _utilisateurService.GetById(id);
            if(utilisateur == null) {
                return NotFound();
            }

            return utilisateur;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Utilisateur>> Create(Utilisateur utilisateur) {
            if(!await _utilisateurService.IsUsernameOrEmailExists(utilisateur.Username, utilisateur.Email)) { 
                Utilisateur utilisateur1 = await _utilisateurService.Create(utilisateur);
                if(utilisateur1 == null) {
                    return BadRequest();
                }
                return Created("", utilisateur1);
            } else {
                return Conflict();
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> Update(Utilisateur utilisateur) => await _utilisateurService.Update(utilisateur);

        [HttpPut("changeprofile")]
        public async Task<ActionResult<bool>> ChangeProfile(Utilisateur utilisateur) => await _utilisateurService.ChangeProfile(utilisateur);


        [HttpPut("changepassword")]
        public async Task<ActionResult<bool>> ChangePassword(Utilisateur utilisateur) => await _utilisateurService.ChangePassword(utilisateur.Id, utilisateur.Password);

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) {
            bool found = await _utilisateurService.Delete(id);

            if(!found) {
                return NotFound();
            }

            return found;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Utilisateur>> Login(Utilisateur utilisateur) {
            Utilisateur utilisateur1 = await _utilisateurService.Login(utilisateur.Username ?? utilisateur.Email, utilisateur.Password);
            if(utilisateur1 == null) {
                return NotFound();
            }

            return utilisateur1;
        }
    }
}
