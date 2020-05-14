
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Services {
    public class UtilisateurService : CrudService<Utilisateur>, IUtilisateurService {
        public UtilisateurService(AppDbContext context) : base(context) {}

        public async Task<Utilisateur> Login(string username, string password) => await Context.Utilisateurs.
                                                                                            FirstOrDefaultAsync(u => (u.Username.Equals(username) && u.Password.Equals(password)) ||
                                                                                                                     (u.Email.Equals(username) && u.Password.Equals(password)));

        public async Task<bool> IsUsernameOrEmailExists(string username, string email) => await Context.Utilisateurs.
                                                                                            FirstOrDefaultAsync(u => u.Username.Equals(username) ||
                                                                                                                     u.Password.Equals(email)) != null;

    }
}
