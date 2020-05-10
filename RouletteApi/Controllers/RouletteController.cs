using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
  [Authorize]
  [Route("api/v2/[controller]")]
  [ApiController]
  public class RouletteController : ControllerBase
  {
    private readonly IRouletteService _rouletteService;

    public RouletteController(IRouletteService rouletteService) =>
      _rouletteService = rouletteService;

    [HttpGet]
    public async Task<IActionResult> GetAllRoulettes() =>
      Ok(await _rouletteService.GetAllRoulettes());

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetRoulette(string id)
    {
      try
      {
        return Ok(await _rouletteService.GetRoulette(id));
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoullete()
    {
      try
      {
        var roulette = await _rouletteService.CreateRoulette();
        return this.StatusCode(
          StatusCodes.Status201Created,
          new { message = $"Roulette with id {roulette.Id} has been created" }
        );
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("open")]
    public async Task<IActionResult> OpenRoulette([FromBody] CloseOpenModel model)
    {
      try
      {
        var roulette = await _rouletteService.OpenRoulette(model.RouletteId);
        return this.StatusCode(
          StatusCodes.Status201Created,
          new { message = $"Roulette with id {roulette.Id} has been opened" }
        );
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("close")]
    public async Task<IActionResult> CloseRoulette([FromBody] CloseOpenModel model)
    {
      try
      {
        BetMessageModel message = await _rouletteService.CloseRoulette(model.RouletteId);
        return this.StatusCode(
          StatusCodes.Status201Created,
          new { message = message.message, message.bets }
        );
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}