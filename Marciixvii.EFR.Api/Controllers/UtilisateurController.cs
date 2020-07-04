using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Models.DTOs;
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
        public async Task<ActionResult<List<Utilisateur>>> GetAll() {
            return Ok(await _utilisateurService.GetAll());
        }

        [HttpGet("getbyid")]
        public async Task<ActionResult<Utilisateur>> GetById(int id) {
            Utilisateur utilisateur = await _utilisateurService.GetById(id);
            if(utilisateur == null) {
                return NotFound("Mawa trop");
            }

            return utilisateur;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Utilisateur>> Create(Utilisateur utilisateur) {
            if(await _utilisateurService.GetIfUsernameOrEmailExists(utilisateur.Username?? utilisateur.Email) == null) {
                return await _utilisateurService.Create(utilisateur);
            } else
                return null;
        }

        [HttpPut("update")]
        public async Task<ActionResult<Utilisateur>> Update(Utilisateur utilisateur) => await _utilisateurService.Update(utilisateur);

        [HttpPut("changeprofil")]
        public async Task<ActionResult<Utilisateur>> ChangeProfile(Utilisateur utilisateur) => await _utilisateurService.ChangeProfile(utilisateur);

        [HttpPut("changepassword")]
        public async Task<ActionResult<Utilisateur>> ChangePassword(UtilisateurNewPassword unp) {
            if(unp.Token != null) { 
                if(!_utilisateurService.IsChangePasswordTokenValid(unp.Token, unp.Username)) {
                    return null; 
                }
            }
            return await _utilisateurService.ChangePassword(unp.Username, unp.Password);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) => await _utilisateurService.Delete(id);

        [HttpPost("login")]
        public async Task<ActionResult<Utilisateur>> Login(Utilisateur utilisateur) {
            return await _utilisateurService.Login(utilisateur.Username ?? utilisateur.Email, utilisateur.Password); 
        }
    }
}
