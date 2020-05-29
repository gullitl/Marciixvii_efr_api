
namespace Marciixvii.EFR.App.Contracts {
    public interface ICryptography {
        byte[] Encrypt(string plain);
        string Decrypt(byte[] cipher);
    }
}
