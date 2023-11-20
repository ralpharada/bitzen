﻿using System.Text;
using System.Security.Cryptography;

namespace Bitzen.Domain.Models
{
    public sealed class Password
    {
        public string Encoded { get; }

        public Password(string password)
        {
            Encoded = EncodePassword(password);
        }

        public static implicit operator Password(string password) => new(password);

        private static string EncodePassword(string password)
        {
            string result;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using var sha256 = new SHA256Managed();
                var hash = sha256.ComputeHash(bytes);
                stream.Write(hash, 0, hash.Length);

                bytes = stream.ToArray();
                result = Convert.ToBase64String(bytes);

            }
            return result;
        }
    }
}
