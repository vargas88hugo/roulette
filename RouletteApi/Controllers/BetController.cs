using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
  [Route("api/v2/[controller]")]
  [ApiController]
  public class BetController : ControllerBase
  {
    private readonly IBetService _betService;

    public BetController(IBetService betService) =>
      _betService = betService;

    [HttpPost("makebet")]
    public async Task<IActionResult> MakeBet(BetModel model)
    {
      try
      {
        var token = Request.Headers["Authorization"];
        var betMessage = await _betService.MakeBet(model, token);
        return this.StatusCode(
          StatusCodes.Status201Created,
          new { message = betMessage.message }
        );
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}