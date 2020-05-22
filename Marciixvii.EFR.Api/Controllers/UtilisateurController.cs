using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UtilisateurController : ControllerBase {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService) {
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
                return await _utilisateurService.Create(utilisateur);
            } else
                return null;
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> Update(Utilisateur utilisateur) => await _utilisateurService.Update(utilisateur);

        [HttpPut("changeprofil")]
        public async Task<ActionResult<bool>> ChangeProfile(Utilisateur utilisateur) => await _utilisateurService.ChangeProfile(utilisateur);

        [HttpPut("changepassword")]
        public async Task<ActionResult<bool>> ChangePassword(Utilisateur utilisateur) => await _utilisateurService.ChangePassword(utilisateur.Id, utilisateur.Password);

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) => await _utilisateurService.Delete(id);

        [HttpPost("login")]
        public async Task<ActionResult<Utilisateur>> Login(Utilisateur utilisateur) => await _utilisateurService.Login(utilisateur.Username ?? utilisateur.Email, utilisateur.Password);
    }
}
