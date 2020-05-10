using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RouletteApi.Helpers;
using RouletteApi.Interfaces;
using RouletteApi.Models;

namespace RouletteApi.Controllers
{
  [Route("api/v2/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;
    private readonly AppSettings _appSettings;

    public AuthController(IAuthService authService, IOptions<AppSettings> appSettings)
    {
      _authService = authService;
      _appSettings = appSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
      await _authService.Register(model);
      return this.StatusCode(
        StatusCodes.Status201Created,
        new { message = $"User with name {model.Username} has been created" }
      );
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateModel model)
    {
      var user = await _authService.Authenticate(model);
      var tokenString = JwtHelper.CreateToken(user, _appSettings);
      return Ok(new
      {
        Id = user.Id,
        Username = user.UserName,
        token = tokenString
      });
    }
  }
}