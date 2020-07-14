using System;
using Marciixvii.EFR.App.Helpers.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marciixvii.EFR.App.Models.Entities {
    [Table("client")]
    public class Client: EntityBase {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Photosrc { get; set; }
        public int NrTelephone { get; set; }
        public string Adresse { get; set; }
        public Sexe Sexe { get; set; }

        public override bool Equals(object obj) {
            return obj is Client client &&
                   Nom == client.Nom &&
                   Prenom == client.Prenom &&
                   Photosrc == client.Photosrc &&
                   NrTelephone == client.NrTelephone &&
                   Sexe == client.Sexe;
        }

        public override int GetHashCode() {
            HashCode hash = new HashCode();
            hash.Add(Nom);
            hash.Add(Prenom);
            hash.Add(Photosrc);
            hash.Add(NrTelephone);
            hash.Add(Sexe);
            return hash.ToHashCode();
        }
    }
}
