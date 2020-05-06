using RouletteApi.Models;
using RouletteApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RouletteApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RoulettesController : ControllerBase
  {
    private readonly RouletteService _rouletteService;

    public RoulettesController(RouletteService rouletteService)
    {
      _rouletteService = rouletteService;
    }

    [HttpGet]
    public ActionResult<List<Roulette>> Get() =>
        _rouletteService.Get();

    [HttpGet("{id:length(24)}", Name = "GetRoulette")]
    public ActionResult<Roulette> Get(string id)
    {
      var roulette = _rouletteService.Get(id);

      if (roulette == null)
      {
        return NotFound();
      }

      return roulette;
    }

    [HttpPost]
    public ActionResult<Roulette> Create(Roulette roulette)
    {
      _rouletteService.Create(roulette);

      return CreatedAtRoute("GetRoulette", new { id = roulette.Id.ToString() }, new { id = roulette.Id.ToString() });
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Roulette rouletteIn)
    {
      var roulette = _rouletteService.Get(id);

      if (roulette == null)
      {
        return NotFound();
      }

      _rouletteService.Update(id, rouletteIn);

      return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
      var roulette = _rouletteService.Get(id);

      if (roulette == null)
      {
        return NotFound();
      }

      _rouletteService.Remove(roulette.Id);

      return NoContent();
    }
  }
}