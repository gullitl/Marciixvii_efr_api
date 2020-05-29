using Marciixvii.EFR.App.Contracts;
using Marciixvii.EFR.App.Helpers.Extentions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Marciixvii.EFR.App.Helpers.Crypt {
    public class DesCryptography : ICryptography {
        private static readonly byte[] des = { 10, 20, 30, 40, 50, 60, 70, 90 };
        private readonly byte[] key;
        private readonly byte[] iv;
        public DesCryptography() {
            key = des;
            iv = des;
        }
        public string Decrypt(byte[] cipher) {
            byte[] inputByteArray;
            try {
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = cipher;

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(Objmst.ToArray());
            } catch(Exception ex) {
                throw ex;
            }
        }

        public byte[] Encrypt(string plain) {
            byte[] inputByteArray;
            try {
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(plain);
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                return Objmst.ToArray();
            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}
