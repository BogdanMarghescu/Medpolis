using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Medpolis
{
    class PasswordEncryption
    {
        private static readonly string hash_code = "mqVYKp6wkrC4BmJAfPLq";

        public string Encrypt(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash_code));
                using (var tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    var result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }

        public string Decrypt(string encrypted_password)
        {
            var data = Convert.FromBase64String(encrypted_password);
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash_code));
                using (var tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    var result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(result, 0, result.Length);
                }
            }
        }
    }
}
