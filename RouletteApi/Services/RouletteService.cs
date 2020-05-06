using RouletteApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApi.Services
{
  public class RouletteService
  {
    private readonly IMongoCollection<Roulette> _roulettes;

    public RouletteService(IRouletteDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _roulettes = database.GetCollection<Roulette>(settings.RoulettesCollectionName);
    }

    public List<Roulette> Get() =>
        _roulettes.Find(roulette => true).ToList();

    public Roulette Get(string id) =>
        _roulettes.Find<Roulette>(roulette => roulette.Id == id).FirstOrDefault();

    public Roulette Create(Roulette roulette)
    {
      _roulettes.InsertOne(roulette);
      return roulette;
    }

    public void Update(string id, Roulette rouletteIn) =>
        _roulettes.ReplaceOne(roulette => roulette.Id == id, rouletteIn);

    public void Remove(Roulette rouletteIn) =>
        _roulettes.DeleteOne(roulette => roulette.Id == rouletteIn.Id);

    public void Remove(string id) =>
        _roulettes.DeleteOne(roulette => roulette.Id == id);
  }
}