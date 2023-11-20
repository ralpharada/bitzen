using System.Security.Cryptography;
using System.Text;

namespace Bitzen.Application.Libraries
{
    public static class StringCipher
    {
        public static string Decrypt(string dados, string EncryptionKey)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(dados);
                using Aes encryptor = Aes.Create();
                Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length); cs.Close();
                }
                dados = Encoding.Unicode.GetString(ms.ToArray());
            }
            catch (Exception ex) { throw ex; }
            return dados;
        }

        public static string Encrypt(string dados, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(dados);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length); cs.Close();
                }
                dados = Convert.ToBase64String(ms.ToArray());
            }
            return dados;
        }
    }
}
