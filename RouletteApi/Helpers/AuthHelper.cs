using System;
using RouletteApi.Models.Entities;

namespace RouletteApi.Helpers
{
  public class AuthHelper
  {
    public static User SetPasswordUser(User user, string password)
    {
      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      return user;
    }
    private static void CreatePasswordHash(
      string password,
      out byte[] passwordHash,
      out byte[] passwordSalt
    )
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    public static void CheckAuthentication(User user, string password)
    {
      if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        throw new Exception("Username or password is incorrect");
    }
    public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i])
            return false;
        }
      }

      return true;
    }
  }
}