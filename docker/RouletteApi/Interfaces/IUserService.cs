using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IUserService
  {
    Task<User> GetUser(string id);
    Task<IEnumerable<User>> GetAllUsers();
  }
}