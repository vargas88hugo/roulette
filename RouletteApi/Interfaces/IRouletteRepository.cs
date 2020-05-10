using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IRouletteRepository
  {
    Task<Roulette> GetRouletteById(string id);
    Task<IEnumerable<Roulette>> GetAllRoulettes();
  }
}