using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteAPI.Models;

namespace RouletteAPI.Interfaces
{
  public interface IUserRepository
  {
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUser(string id);
    Task<User> CreateUser(User user, string password);
    Task<User> UpdateUserMoney(User user, int money);
  }
}