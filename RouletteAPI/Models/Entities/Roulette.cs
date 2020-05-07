using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteAPI.Models
{
  public class Roulette
  {
    public Roulette()
    {
      Status = "Close";
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Status { get; set; }

    public void OpenGame()
    {
      Status = "Open";
    }

    public void CloseGame()
    {
      Status = "Close";
    }
  }
}