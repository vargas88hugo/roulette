using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models.Entities
{
  public class Roulette
  {
    public Roulette()
    {
      Bets = new List<BetRoulette>();
      Status = "Close";
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Status { get; set; }
    public List<BetRoulette> Bets { get; set; }


    public void OpenGame() =>
      Status = "Open";

    public void CloseGame() =>
      Status = "Close";

    public void AddBet(BetRoulette bet) =>
      Bets.Add(bet);
  }
}