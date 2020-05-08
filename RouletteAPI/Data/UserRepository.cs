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

      var user = await _context.Users.Find(x => x.Username == username)
        .FirstOrDefaultAsync();
      UserHandler.CheckAuthentication(user, password);
      return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
      return await _context.Users.Find(_ => true).ToListAsync();
    }

    public async Task<User> GetUser(string id)
    {
      return await _context.Users.Find(user => user.Id == id.ToString())
        .FirstOrDefaultAsync();
    }

    public async Task<User> CreateUser(User user, string password)
    {
      UserHandler.CheckRegistration(password);
      byte[] passwordHash, passwordSalt;
      PasswordHandler.CreatePasswordHash(password, out passwordHash, out passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      await _context.Users.InsertOneAsync(user);
      return user;
    }
  }
}
