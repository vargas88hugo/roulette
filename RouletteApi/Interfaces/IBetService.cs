using System.Threading.Tasks;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Interfaces
{
  public interface IBetService
  {
    Task<MakeBetMessageModel> MakeBet(
      BetModel model,
      string token
    );
    Task<Roulette> ConfigureRoulette(BetModel model, string userId);
    Task<User> ConfigureUser(BetModel model, string userId);
    Task UpdateUserRoulette(User user, Roulette roulette);
  }
}