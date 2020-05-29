
namespace Marciixvii.EFR.App.Contracts {
    public interface ICryptography {
        byte[] Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
