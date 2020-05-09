using RouletteAPI.Models;

namespace RouletteAPI.Helpers
{
  public class BetHandler
  {
    public static void CheckBetRoulette(BetRoulette betRoulette, string rouletteStatus)
    {
      var bet = betRoulette.Number;
      var money = betRoulette.Money;
      CheckBet(betRoulette.Number);
      CheckMoney(betRoulette.Money);
      CheckRoulette(rouletteStatus);
      CheckColor(betRoulette.Color);
    }
    private static void CheckBet(int bet)
    {
      if (!(bet >= 0 && bet <= 36))
        throw new AppException("Bet number is not allowed. The limit is from 0 to 36");
    }

    private static void CheckMoney(int money)
    {
      if (!(money >= 0 && money <= 10000))
        throw new AppException("Money amount is not allowed. The limit is from 0 to 10000");
    }

    private static void CheckRoulette(string status)
    {
      if (status != "Open")
        throw new AppException("The roulette is closed");
    }

    private static void CheckColor(string color)
    {
      if (!color.ToLower().Equals("red") && !color.ToLower().Equals("black"))
        throw new AppException("Color is not allowed. Please choose Red or Black");
    }
  }
}