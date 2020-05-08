using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RouletteAPI.Models;
using System.Threading.Tasks;
using RouletteAPI.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RouletteAPI.Helpers;

namespace RouletteAPI.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly UserContext _context;

    public UserRepository(IOptions<Settings> settings)
    {
      _context = new UserContext(settings);
    }

    public async Task<User> Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = await _context.Users.Find(x => x.Username == username)
        .FirstOrDefaultAsync();

      // check if username exists
      if (user == null)
        return null;

      // check if password is correct
      if (!PasswordHandler.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful
      return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
      try
      {
        return await _context.Users.Find(_ => true).ToListAsync();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<User> GetUser(string id)
    {
      try
      {
        return await _context.Users.Find(user => user.Id == id.ToString())
          .FirstOrDefaultAsync();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<User> CreateUser(User user, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

      // var existUser = await _context.Users.FindAsync(x => x.Username == user.Username);

      // if (existUser != null)
      //   throw new AppException("Username \"" + user.Username + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      PasswordHandler.CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await _context.Users.InsertOneAsync(user);
      return user;
    }
  }
}
