using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RouletteAPI.Helpers
{
  public class JwtHandler
  {
    public static string ParseToken(string authHeader)
    {
      string token = authHeader.Substring("Bearer ".Length).Trim();
      string secret = "Super Secret Key Enjoy";
      var key = Encoding.ASCII.GetBytes(secret);
      var handler = new JwtSecurityTokenHandler();
      var validations = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
      };
      var claims = handler.ValidateToken(token, validations, out var tokenSecure);
      return claims.Identity.Name;
    }
  }
}