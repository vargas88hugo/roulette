using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using System;

namespace RouletteAPI.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class RoulettesController : ControllerBase
  {
    private readonly IRouletteRepository _rouletteRepository;

    public RoulettesController(IRouletteRepository rouletteRepository)
    {
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
      {
        return NotFound();
      }
      return CreatedAtRoute("GetRoulette",
        new { id = roulette.Id.ToString() },
        new { message = $"Roulette with id {roulette.Id} has been Closed" }
      );
    }
  }
}