using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using RouletteAPI.Helpers;

namespace RouletteAPI.Data
{
  public class RouletteRepository : IRouletteRepository
  {
    private readonly RouletteContext _context = null;

    public RouletteRepository(IOptions<Settings> settings)
    {
      _context = new RouletteContext(settings);
    }

    public async Task<IEnumerable<Roulette>> GetAllRoulettes()
    {
      return await _context.Roulettes.Find(_ => true).ToListAsync();
    }

    public async Task<Roulette> GetRoulette(string id)
    {
      return await _context.Roulettes.Find(roulette => roulette.Id == id)
        .FirstOrDefaultAsync();
    }

    public async Task<Roulette> CreateRoulette()
    {
      Roulette roulette = new Roulette();
      await _context.Roulettes.InsertOneAsync(roulette);
      return roulette;
    }

    public async Task<Roulette> OpenRoulette(string id)
    {
      Roulette roulette = await this.GetRoulette(id);
      roulette.Status = "Open";
      await _context.Roulettes.ReplaceOneAsync(roulette => roulette.Id == id, roulette);
      return roulette;
    }

    public async Task<Roulette> CloseRoulette(string id)
    {
      Roulette roulette = await this.GetRoulette(id);
      roulette.Status = "Close";
      await _context.Roulettes.ReplaceOneAsync(roulette => roulette.Id == id, roulette);
      return roulette;
    }

    public async Task<Roulette> MakeBetRoulette(BetRoulette bet, string userId)
    {
      Roulette roulette = await this.GetRoulette(bet.RouletteId);
      BetHandler.CheckBetRoulette(bet, roulette.Status);
      bet.UserId = userId;
      roulette.AddBet(bet);
      await _context.Roulettes.ReplaceOneAsync(
        roulette => roulette.Id == bet.RouletteId,
        roulette
      );
      return roulette;
    }
  }
}
