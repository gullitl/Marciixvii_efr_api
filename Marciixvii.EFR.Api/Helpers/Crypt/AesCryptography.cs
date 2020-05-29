using Marciixvii.EFR.App.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Marciixvii.EFR.App.Helpers.Crypt {
    public class AesCryptography : ICryptography {
        private readonly AesManaged aes;

        public AesCryptography() {
            aes  = new AesManaged();
        }
        public string Decrypt(byte[] cipher) {
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(cipher);
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }

        public byte[] Encrypt(string plain) {
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using StreamWriter sw = new StreamWriter(cs);
            sw.Write(plain);
            return ms.ToArray();
        }
    }
}
