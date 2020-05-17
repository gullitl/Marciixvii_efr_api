
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<bool> ChangePassword(int id, string password) {
            Utilisateur utilisateur = new Utilisateur { Id = id, Password = password };
            Context.Utilisateurs.Attach(utilisateur);
            Context.Entry(utilisateur).Property(u => u.Password).IsModified = true;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeProfile(Utilisateur utilisateur) {
            Context.Utilisateurs.Attach(utilisateur);
            EntityEntry<Utilisateur> contextEntry = Context.Entry(utilisateur);
            contextEntry.Property(u => u.Nom).IsModified = true;
            contextEntry.Property(u => u.Postnom).IsModified = true;
            contextEntry.Property(u => u.Prenom).IsModified = true;
            contextEntry.Property(u => u.Sexe).IsModified = true;
            contextEntry.Property(u => u.Email).IsModified = true;
            contextEntry.Property(u => u.Username).IsModified = true;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
