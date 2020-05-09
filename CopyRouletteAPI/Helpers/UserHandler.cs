using MongoDB.Driver;
using RouletteAPI.Models;

namespace RouletteAPI.Helpers
{
  public class UserHandler
  {
    public static void CheckAuthentication(User user, string password)
    {
      if (user == null)
        throw new AppException("Username or password is incorrect");
      if (!PasswordHandler.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        throw new AppException("Username or password is incorrect");
    }

    public static void CheckRegistration(string password)
    {
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");
    }
  }
}