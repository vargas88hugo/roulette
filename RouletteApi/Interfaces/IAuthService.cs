using System.Threading.Tasks;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IAuthService
  {
    Task<User> Register(RegisterModel user);
  }
}