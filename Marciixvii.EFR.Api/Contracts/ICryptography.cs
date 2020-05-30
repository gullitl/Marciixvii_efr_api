
namespace Marciixvii.EFR.App.Contracts {
    public interface ICryptography {
        string Encrypt(string plain);
        string Decrypt(string cipher);
    }
}
