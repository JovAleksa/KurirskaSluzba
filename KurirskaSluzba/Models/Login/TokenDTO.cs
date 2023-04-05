using System;

namespace KurirskaSluzba.Models.Login
{
    public class TokenDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public string Email { get; set; }

    }
}
