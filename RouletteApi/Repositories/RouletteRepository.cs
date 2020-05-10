using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteApi.Interfaces;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories
{
  public class RouletteRepository : IRouletteRepository
  {
    private readonly RouletteContext _context;

    public RouletteRepository(IOptions<Settings> settings) =>
      _context = new RouletteContext(settings);

    public async Task<Roulette> GetRouletteById(string id) =>
      await _context.Roulettes.Find(roulette => roulette.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<Roulette>> GetAllRoulettes() =>
      await _context.Roulettes.Find(_ => true).ToListAsync();

    public async Task InsertRoulette(Roulette roulette) =>
      await _context.Roulettes.InsertOneAsync(roulette);

    public async Task ReplaceRoulette(Roulette newRoulette) =>
      await _context.Roulettes
        .ReplaceOneAsync(roulette => roulette.Id == newRoulette.Id, newRoulette);
  }
}