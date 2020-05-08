using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteAPI.Models
{
  public class Roulette
  {
    private List<BetRoulette> bets;

    public Roulette()
    {
      bets = new List<BetRoulette>();
      Status = "Close";
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Status { get; set; }

    public void OpenGame() =>
      Status = "Open";

    public void CloseGame() =>
      Status = "Close";

    public void AddBet(BetRoulette bet) =>
      bets.Add(bet);
  }
}