using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RouletteAPI.Models;
using System.Threading.Tasks;
using RouletteAPI.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

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
  }
}
