using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Helpers;
using RouletteApi.Interfaces;
using RouletteApi.Models;
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

    public async Task<Roulette> GetRoulette(string id)
    {
      var roulette = await _rouletteRepository.GetRouletteById(id);
      if (!RouletteHelper.existRoulette(roulette))
        throw new Exception($"Roulette with id {id} not found");
      return roulette;
    }

    public async Task<Roulette> CreateRoulette()
    {
      var roulette = new Roulette();
      await _rouletteRepository.InsertRoulette(roulette);
      return roulette;
    }

    public async Task<Roulette> OpenRoulette(string rouletteId)
    {
      var roulette = await _rouletteRepository.GetRouletteById(rouletteId);
      RouletteHelper.ConfigureOpenRoulette(roulette, rouletteId);
      await _rouletteRepository.UpdateRoulette(roulette);
      return roulette;
    }
  }
}