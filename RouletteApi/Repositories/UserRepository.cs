using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteApi.Interfaces;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly UserContext _context;

    public UserRepository(IOptions<Settings> settings) =>
      _context = new UserContext(settings);

    public async Task<IEnumerable<User>> GetAllUsers() =>
      await _context.Users.Find(_ => true).ToListAsync();
    public async Task<User> GetUser(string id) =>
      await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
    public async Task<User> InsertUser(User user)
    {
      await _context.Users.InsertOneAsync(user);
      return user;
    }
  }
}