using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using RouletteAPI.Helpers;

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

    [HttpGet("{id:length(24)}", Name = "Get")]
    public async Task<IActionResult> Get(string id)
    {
      Roulette roulette = await _rouletteRepository.GetRoulette(id);
      if (roulette == null)
      {
        NotFound();
      }
      return Ok(roulette);
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
      var roulette = await _rouletteRepository.CreateRoulette();
      if (roulette == null)
      {
        BadRequest();
      }
      return CreatedAtRoute("Get",
        new { id = roulette.Id.ToString() },
        new { message = $"roulette with id {roulette.Id} has been created" });
    }
  }
}