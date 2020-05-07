using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteAPI.Models;

namespace RouletteAPI.Interfaces
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUser(string id);
    Task<User> CreateUser(User user);
  }
}