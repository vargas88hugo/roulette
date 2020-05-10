using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Interfaces;
using RouletteApi.Models.Entities;

namespace RouletteApi.Services
{
  public class RouletteService : IRouletteService
  {
    private readonly IRouletteRepository _rouletteRepository;

    public RouletteService(IRouletteRepository rouletteRepository) =>
      _rouletteRepository = rouletteRepository;

    public async Task<IEnumerable<Roulette>> GetAllRoulettes() =>
      await _rouletteRepository.GetAllRoulettes();

    public async Task<Roulette> GetRoulette(string id) =>
      await _rouletteRepository.GetRouletteById(id);
  }
}