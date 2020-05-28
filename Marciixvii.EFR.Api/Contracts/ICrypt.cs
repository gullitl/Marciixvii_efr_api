
namespace Marciixvii.EFR.App.Contracts {
    public interface ICrypt {
        byte[] Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
