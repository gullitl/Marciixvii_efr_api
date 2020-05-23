
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Services {
    public class UtilisateurService : CrudService<Utilisateur>, IUtilisateurService {
        public UtilisateurService(ILogger<CrudService<Utilisateur>> logger, AppDbContext context) : base(logger, context) {
        }

        public async Task<Utilisateur> Login(string username, string password) {
            try {
                return await Context.Utilisateurs.
                       FirstOrDefaultAsync(u => (u.Username.Equals(username) && u.Password.Equals(password)) ||
                                                (u.Email.Equals(username) && u.Password.Equals(password)));
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> IsUsernameOrEmailExists(string username, string email) {
            try {
                return await Context.Utilisateurs.FirstOrDefaultAsync(u => u.Username.Equals(username) ||
                                                                      u.Password.Equals(email)) != null;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangePassword(int id, string password) {
            try {
                Utilisateur utilisateur = new Utilisateur { Id = id, Password = password };
                Context.Utilisateurs.Attach(utilisateur);
                Context.Entry(utilisateur).Property(u => u.Password).IsModified = true;
                await Context.SaveChangesAsync();
                return true;
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                return false;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> ChangeProfile(Utilisateur utilisateur) {
            try {
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
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                return false;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                return false;
            }
            
        }
    }
}
