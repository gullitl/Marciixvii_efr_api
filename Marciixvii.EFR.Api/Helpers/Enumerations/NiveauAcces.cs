using System.ComponentModel;

namespace Marciixvii.EFR.App.Helpers.Enumerations {
    public enum NiveauAcces {
        [Description("Administrateur")] Administrateur = 1,
        [Description("Utilisateur")] Utilisateur = 2
    }
}
