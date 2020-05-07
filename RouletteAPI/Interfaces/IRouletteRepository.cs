using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteAPI.Models;

namespace RouletteAPI.Interfaces
{
  public interface IRouletteRepository
  {
    Task<IEnumerable<Roulette>> GetAllRoulettes();
    Task<Roulette> GetRoulette(string id);
    Task<Roulette> CreateRoulette();
  }
}