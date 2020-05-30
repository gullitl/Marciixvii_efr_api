using Marciixvii.EFR.App.Contracts;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Marciixvii.EFR.App.Helpers.Crypt {
    public class AesCryptography : ICryptography {
        private readonly AesManaged aes;
        private readonly Encoding encoding;

        public AesCryptography() {
            aes  = new AesManaged();
            encoding = Encoding.UTF8;
        }
        public string Decrypt(string cipher) {
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(encoding.GetBytes(cipher));
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }

        public string Encrypt(string plain) {
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using StreamWriter sw = new StreamWriter(cs);
            sw.Write(plain);
            return encoding.GetString(ms.ToArray());
        }
    }
}
