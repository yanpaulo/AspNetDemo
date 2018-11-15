using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AspNetDemo.Services
{
    public class TokenConfigurations
    {
        public string Issuer { get; set; } = "YanSoft";

        public string Audience { get; set; } = "YanSoft.Audience";

        public SecurityKey SecurityKey { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public TokenValidationParameters TokenValidationParameters { get; set; }


        public TokenConfigurations()
        {
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{59338A60-37A5-46D9-98F2-420B1CB3B851}"));
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = SecurityKey,
                ValidAudience = Audience,
                ValidIssuer = Issuer,

                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

    }
}
