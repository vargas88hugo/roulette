using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IRouletteService
  {
    Task<Roulette> GetRoulette(string id);
    Task<IEnumerable<Roulette>> GetAllRoulettes();
    Task<Roulette> CreateRoulette();
    Task<Roulette> OpenRoulette(string id);
    Task<BetMessageModel> CloseRoulette(string id);
  }
}