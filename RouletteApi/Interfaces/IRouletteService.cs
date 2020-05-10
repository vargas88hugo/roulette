using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IRouletteService
  {
    Task<Roulette> GetRoulette(string id);
    Task<IEnumerable<Roulette>> GetAllRoulettes();
  }
}