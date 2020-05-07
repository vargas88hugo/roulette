using RouletteApi.Models;
using RouletteApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

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

    [HttpGet("create")]
    public ActionResult<object> CreateRoulete()
    {
      Roulette roulette = _rouletteService.Create();

      return Created($"/{roulette.Id}", new { Id = roulette.Id, Status = 201 });
    }

    [HttpPut("open/{id:length(24)}")]
    public ActionResult<object> OpenRoulette(string id)
    {
      var roulette = _rouletteService.Get(id);
      if (roulette == null)
      {
        return NotFound();
      }
      _rouletteService.OpenRoulette(id);
      return Created($"/{id}", new { Message = "Roulette Successfully Opened", Status = 201 });
    }

    [HttpPut("close/{id:length(24)}")]
    public IActionResult CloseRoulette(string id)
    {
      var roulette = _rouletteService.Get(id);

      if (roulette == null)
      {
        return NotFound();
      }

      _rouletteService.CloseRoulette(id);

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