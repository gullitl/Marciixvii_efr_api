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

        public UtilisateurController(ILogger<UtilisateurController> logger,
            IUtilisateurService utilisateurService) {
            _logger = logger;
            _utilisateurService = utilisateurService;
        }

        [HttpGet("getallutilisateurs")]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAll() {
            return await _utilisateurService.GetAll();
        }

        [HttpGet("getutilisateurbyid/{id}")]
        public async Task<ActionResult<Utilisateur>> GetById(int id) {
            Utilisateur utilisateur = await _utilisateurService.GetById(id);
            if(utilisateur == null) {
                return NotFound();
            }

            return utilisateur;
        }

        [HttpPost("saveutilisateur")]
        public async Task<ActionResult<Utilisateur>> Create(Utilisateur utilisateur) {
            Utilisateur utilisateur1 = await _utilisateurService.Create(utilisateur);
            if(utilisateur1 == null) {
                return BadRequest();
            }
            return Created("", utilisateur1);
        }

        [HttpPut("updateutilisateur")]
        public async Task<ActionResult<bool>> Update(Utilisateur utilisateur) {
            return await _utilisateurService.Update(utilisateur);
        }

        [HttpDelete("deleteutilisateurbyid/{id}")]
        public async Task<ActionResult<bool>> Delete(int id) {
            bool found = await _utilisateurService.Delete(id);

            if(!found) {
                return NotFound();
            }

            return found;
        }

        [HttpGet("login/{username}/{password}")]
        public async Task<ActionResult<Utilisateur>> Login(string username, string password) {
            Utilisateur utilisateur = await _utilisateurService.Login(username, password);
            if(utilisateur == null) {
                return NotFound();
            }

            return utilisateur;
        }
    }
}
