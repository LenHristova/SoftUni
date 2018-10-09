namespace IRunes.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Contracts;

    public class HashService : IHashService
    {
        public string ComputeHash(string stringToHash)
        {
            stringToHash += "myAppSalt15648984196849#";

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
