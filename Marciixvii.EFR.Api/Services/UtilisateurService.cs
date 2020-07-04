
using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.DataAccess.Contexts;
using Marciixvii.EFR.App.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Services {
    public class UtilisateurService : CrudService<Utilisateur>, IUtilisateurService {
        private readonly ICryptography DesCryptography;
        public UtilisateurService(ILogger<CrudService<Utilisateur>> logger, 
                                  AppDbContext context, 
                                  ICryptography desCryptography) : base(logger, context) {
            DesCryptography = desCryptography;
        }

        public async Task<Utilisateur> Login(string username, string password) {
            try {
                return await Context.Utilisateurs.
                       FirstOrDefaultAsync(u => (u.Username.Equals(username) && u.Password.Equals(password)) ||
                                                (u.Email.Equals(username) && u.Password.Equals(password)));
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Utilisateur> GetIfUsernameOrEmailExists(string usernameOrEmail) {
            try {
                return await Context.Utilisateurs.FirstOrDefaultAsync(u => u.Username.Equals(usernameOrEmail) || u.Email.Equals(usernameOrEmail));
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Utilisateur> ChangePassword(string usernameOrEmail, string password) {
            try {
                Utilisateur utilisateur = await GetIfUsernameOrEmailExists(usernameOrEmail);
                if(utilisateur != null) {
                    utilisateur.Password = password;
                    await Context.SaveChangesAsync();
                    return utilisateur;
                } else
                    return null;
                
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                throw ex;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public bool IsChangePasswordTokenValid(string token, string usernameOrEmail) {
            try {
                string plain = DesCryptography.Decrypt(token);
                string[] flats = plain.Split('#');
                string changePasswordToken = flats[0];
                double timeout = double.Parse(flats[1]);
                DateTime oDate = DateTime.Parse(flats[2]);

                if(changePasswordToken != usernameOrEmail) {
                    return false;
                }

                DateTime datetimeout = oDate.AddMinutes(timeout);
                if(datetimeout.CompareTo(DateTime.Now) < 0) {
                    return false;
                }
                return true;
            } catch(CryptographicException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Utilisateur> ChangeProfile(Utilisateur utilisateur) {
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
                return utilisateur;
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                throw ex;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
            
        }
    }
}
