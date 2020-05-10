using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IUserRepository
  {
    Task<User> GetUserById(string id);
    Task<User> GetUserByName(string name);
    Task<IEnumerable<User>> GetAllUsers();
    Task InsertUser(User user);
    Task UpdateUser(User user);
  }
}