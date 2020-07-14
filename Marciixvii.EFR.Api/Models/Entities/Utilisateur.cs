using System;
using Marciixvii.EFR.App.Helpers.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marciixvii.EFR.App.Models.Entities {
    [Table("utilisateur")]
    public class Utilisateur : EntityBase {
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public Sexe Sexe { get; set; }
        public string Photosrc { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public NiveauAcces NiveauAcces { get; set; }

        public override bool Equals(object obj) {
            return obj is Utilisateur utilisateur &&
                   Nom == utilisateur.Nom &&
                   Postnom == utilisateur.Postnom &&
                   Prenom == utilisateur.Prenom &&
                   Sexe == utilisateur.Sexe &&
                   Photosrc == utilisateur.Photosrc &&
                   Email == utilisateur.Email &&
                   Username == utilisateur.Username &&
                   Password == utilisateur.Password &&
                   NiveauAcces == utilisateur.NiveauAcces;
        }

        public override int GetHashCode() {
            HashCode hash = new HashCode();
            hash.Add(Nom);
            hash.Add(Postnom);
            hash.Add(Prenom);
            hash.Add(Sexe);
            hash.Add(Photosrc);
            hash.Add(Email);
            hash.Add(Username);
            hash.Add(Password);
            hash.Add(NiveauAcces);
            return hash.ToHashCode();
        }
    }
}
