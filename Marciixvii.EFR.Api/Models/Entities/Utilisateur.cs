using Marciixvii.EFR.App.Helpers.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Marciixvii.EFR.App.Models.Entities {
    [Table("utilisateur")]
    public class Utilisateur : EntityBase{
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public Sexe Sexe { get; set; }
        public string Photosrc { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public NiveauAcces NiveauAcces { get; set; }

    }
}
