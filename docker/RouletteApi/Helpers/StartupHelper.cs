using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RouletteApi.Interfaces;

namespace RouletteApi.Helpers
{
  public class StartupHelper
  {
    public static void ValidateToken(JwtBearerOptions options, byte[] key)
    {
      options.Events = new JwtBearerEvents
      {
        OnTokenValidated = context =>
        {
          var userService = context.HttpContext.RequestServices
            .GetRequiredService<IUserRepository>();
          var userId = context.Principal.Identity.Name;
          var user = userService.GetUserById(userId);
          if (user == null)
            context.Fail("Unauthorized");
          return Task.CompletedTask;
        }
      };
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    }
  }
}