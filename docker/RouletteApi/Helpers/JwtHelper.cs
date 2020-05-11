using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RouletteApi.Models.Entities;

namespace RouletteApi.Helpers
{
  public class JwtHelper
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

    public static string CreateToken(User user, AppSettings _appSettings)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}