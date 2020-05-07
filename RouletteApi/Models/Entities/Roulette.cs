using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models
{
  public class Roulette
  {
    private List<Bet> bets;
    public Roulette()
    {
      Status = "Close";
      List<Bet> bets = new List<Bet>();
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

    public void AddBet(Bet bet)
    {
      bets.Add(bet);
    }
  }
}