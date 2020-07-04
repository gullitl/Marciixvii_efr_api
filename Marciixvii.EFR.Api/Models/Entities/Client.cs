using System;
using Marciixvii.EFR.App.Helpers.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marciixvii.EFR.App.Models.Entities {
    [Table("client")]
    public class Client: EntityBase {
        public string NomComplet { get; set; }
        public int Telephone { get; set; }
        public string Adresse { get; set; }
        public Sexe Sexe { get; set; }

        public override bool Equals(object obj) {
            return obj is Client client &&
                   NomComplet == client.NomComplet &&
                   Telephone == client.Telephone &&
                   Sexe == client.Sexe;
        }

        public override int GetHashCode() {
            HashCode hash = new HashCode();
            hash.Add(NomComplet);
            hash.Add(Telephone);
            hash.Add(Sexe);
            return hash.ToHashCode();
        }
    }
}
