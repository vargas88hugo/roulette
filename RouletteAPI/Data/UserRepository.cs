using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RouletteAPI.Models;
using System.Threading.Tasks;
using RouletteAPI.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace RouletteAPI.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly UserContext _context = null;

    public UserRepository(IOptions<Settings> settings)
    {
      _context = new UserContext(settings);
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
  }
}
