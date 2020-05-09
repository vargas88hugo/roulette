using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouletteAPI.Models;

public class RouletteContext
{
  private readonly IMongoDatabase _database = null;

  public RouletteContext(IOptions<Settings> settings)
  {
    var client = new MongoClient(settings.Value.ConnectionString);
    if (client != null)
      _database = client.GetDatabase(settings.Value.Database);
  }

  public IMongoCollection<Roulette> Roulettes
  {
    get
    {
      return _database.GetCollection<Roulette>("Roulettes");
    }
  }
}