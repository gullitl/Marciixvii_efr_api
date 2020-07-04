using Marciixvii.EFR.App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marciixvii.EFR.App.DataAccess.Contexts {
    public class AppDbContext : DbContext {

        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}
