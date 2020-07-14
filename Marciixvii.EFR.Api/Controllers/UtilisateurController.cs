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
        private readonly IUtilisateurService UtilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService) {
            UtilisateurService = utilisateurService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<Utilisateur>>> GetAll() => Ok(await UtilisateurService.GetAll());

        [HttpGet("getbyid")]
        public async Task<ActionResult<Utilisateur>> GetById(int id) {
            Utilisateur utilisateur = await UtilisateurService.GetById(id);
            if(utilisateur == null) {
                return NotFound("Mawa trop");
            }

            return utilisateur;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Utilisateur>> Create(Utilisateur utilisateur) {
            if(await UtilisateurService.GetIfUsernameOrEmailExists(utilisateur.Username?? utilisateur.Email) == null) {
                return await UtilisateurService.Create(utilisateur);
            } else
                return null;
        }

        [HttpPut("update")]
        public async Task<ActionResult<Utilisateur>> Update(Utilisateur utilisateur) => await UtilisateurService.Update(utilisateur);

        [HttpPut("changeprofil")]
        public async Task<ActionResult<Utilisateur>> ChangeProfile(Utilisateur utilisateur) => await UtilisateurService.ChangeProfile(utilisateur);

        [HttpPut("changepassword")]
        public async Task<ActionResult<Utilisateur>> ChangePassword(UtilisateurNewPassword unp) {
            if(unp.Token != null) { 
                if(!UtilisateurService.IsChangePasswordTokenValid(unp.Token, unp.Username)) {
                    return null; 
                }
            }
            return await UtilisateurService.ChangePassword(unp.Username, unp.Password);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> Delete(int id) => await UtilisateurService.Delete(id);

        [HttpPost("login")]
        public async Task<ActionResult<Utilisateur>> Login(Utilisateur utilisateur) => await UtilisateurService.Login(utilisateur.Username ?? utilisateur.Email, utilisateur.Password); 
    }
}
