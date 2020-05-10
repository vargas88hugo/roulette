using System.Threading.Tasks;
using RouletteApi.Interfaces;
using RouletteApi.Models;
using RouletteApi.Models.Entities;
using RouletteApi.Helpers;

namespace RouletteApi.Services
{
  public class BetService : IBetService
  {
    private readonly IRouletteRepository _rouletteRepository;
    private readonly IUserRepository _userRepository;

    public BetService(
      IRouletteRepository rouletteRepository,
      IUserRepository userRepository
    )
    {
      _rouletteRepository = rouletteRepository;
      _userRepository = userRepository;
    }

    public async Task<MakeBetMessageModel> MakeBet(BetModel model, string token)
    {
      var userId = JwtHelper.ParseToken(token);
      var roulette = await this.ConfigureRoulette(model, userId);
      var user = await this.ConfigureUser(model, userId);
      await this.UpdateUserRoulette(user, roulette);
      return new MakeBetMessageModel(
        $"You bet ${model.Money} on {model.Color} {model.Number}. Good luck!"
      );
    }

    public async Task<Roulette> ConfigureRoulette(BetModel model, string userId)
    {
      Roulette roulette = await _rouletteRepository.GetRouletteById(model.RouletteId);
      RouletteHelper.CheckRoulette(roulette, model.RouletteId);
      BetRoulette bet = new BetRoulette(model, userId);
      roulette.AddBet(bet);
      return roulette;
    }

    public async Task<User> ConfigureUser(BetModel model, string userId)
    {
      User user = await _userRepository.GetUserById(userId);
      UserHelper.CheckUser(user, userId, model.Money);
      user.Money = user.Money - model.Money;
      return user;
    }

    public async Task UpdateUserRoulette(User user, Roulette roulette)
    {
      await _userRepository.UpdateUser(user);
      await _rouletteRepository.UpdateRoulette(roulette);
    }
  }
}