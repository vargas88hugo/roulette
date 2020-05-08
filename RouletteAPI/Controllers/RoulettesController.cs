using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using RouletteAPI.Helpers;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RouletteAPI.Controllers
{
  [Authorize]
  [Route("api/v1/[controller]")]
  [ApiController]
  public class RoulettesController : ControllerBase
  {
    private readonly IRouletteRepository _rouletteRepository;
    private readonly IUserRepository _userRepository;

    public RoulettesController(
      IRouletteRepository rouletteRepository,
      IUserRepository userRepository
    )
    {
      _userRepository = userRepository;
      _rouletteRepository = rouletteRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Roulette>> Get() =>
      await _rouletteRepository.GetAllRoulettes();

    [HttpGet("{id:length(24)}", Name = "GetRoulette")]
    public async Task<IActionResult> Get(string id)
    {
      Roulette roulette = await _rouletteRepository.GetRoulette(id);
      if (roulette == null)
      {
        return NotFound();
      }
      return Ok(roulette);
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
      var roulette = await _rouletteRepository.CreateRoulette();
      if (roulette == null)
      {
        return BadRequest();
      }
      return CreatedAtRoute("GetRoulette",
        new { id = roulette.Id.ToString() },
        new { message = $"Roulette with id {roulette.Id} has been created" });
    }

    [HttpPut("open")]
    public async Task<IActionResult> PutOpen([FromBody] Roulette objId)
    {
      var roulette = await _rouletteRepository.OpenRoulette(objId.Id);
      if (roulette == null)
      {
        return NotFound();
      }
      return CreatedAtRoute("GetRoulette",
        new { id = roulette.Id.ToString() },
        new { message = $"Roulette with id {roulette.Id} has been Opened" }
      );
    }

    [HttpPut("close")]
    public async Task<IActionResult> PutClose([FromBody] Roulette objId)
    {
      var roulette = await _rouletteRepository.CloseRoulette(objId.Id);
      if (roulette == null)
        return NotFound();
      var result = RouletteHandler.ChooseWinningBet(roulette);
      return CreatedAtRoute("GetRoulette",
        new { id = roulette.Id.ToString() },
        new { message = result }
      );
    }

    [HttpGet("bet")]
    public async Task<IActionResult> BetRoulette(BetRoulette bet)
    {
      try
      {
        var token = Request.Headers["Authorization"];
        var userId = JwtHandler.ParseToken(token);
        var user = await _userRepository.GetUser(userId);
        if (user.Money < bet.Money)
          throw new AppException("You don't have enough money");
        await _userRepository.UpdateUserMoney(user, bet.Money);
        await _rouletteRepository.MakeBetRoulette(bet, userId);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message.ToString() });
      }
      return CreatedAtRoute(
        "GetRoulette",
        new { id = bet.RouletteId },
        new { message = $"You bet ${bet.Money} on {bet.Color} {bet.Bet}. Good luck!" }
      );
    }
  }
}