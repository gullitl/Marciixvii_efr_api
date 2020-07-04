using Marciixvii.EFR.App.Models.Entities;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Contracts {
    public interface IUtilisateurService : ICrud<Utilisateur> {
        Task<Utilisateur> Login(string username, string password);
        Task<Utilisateur> GetIfUsernameOrEmailExists(string usernameOrEmail);
        Task<Utilisateur> ChangePassword(string id, string password);
        Task<Utilisateur> ChangeProfile(Utilisateur utilisateur);
        bool IsChangePasswordTokenValid(string token, string usernameOrEmail);
    }
}
