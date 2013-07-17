using System;

namespace PassCache.Models
{
    public class SetModel
    {
        public readonly string Random;
        public Guid Guid;

        public SetModel()
        {
            var randomBytes = new byte[128];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(randomBytes);
            Random = Convert.ToBase64String(randomBytes);
            Guid = Guid.NewGuid();
        }
    }
}