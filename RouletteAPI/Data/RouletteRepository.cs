using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;

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
      try
      {
        return await _context.Roulettes.Find(_ => true).ToListAsync();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<Roulette> GetRoulette(string id)
    {
      try
      {
        return await _context.Roulettes.Find(roulette => roulette.Id == id)
          .FirstOrDefaultAsync();
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<Roulette> CreateRoulette()
    {
      try
      {
        Roulette roulette = new Roulette();
        await _context.Roulettes.InsertOneAsync(roulette);
        return roulette;
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<Roulette> OpenRoulette(string id)
    {
      try
      {
        Roulette roulette = await this.GetRoulette(id);
        roulette.Status = "Open";
        await _context.Roulettes.ReplaceOneAsync(roulette => roulette.Id == id, roulette);
        return roulette;
      }
      catch
      {
        return null;
      }
    }

    public async Task<Roulette> CloseRoulette(string id)
    {
      try
      {
        Roulette roulette = await this.GetRoulette(id);
        roulette.Status = "Close";
        await _context.Roulettes.ReplaceOneAsync(roulette => roulette.Id == id, roulette);
        return roulette;
      }
      catch
      {
        return null;
      }
    }
  }
}
