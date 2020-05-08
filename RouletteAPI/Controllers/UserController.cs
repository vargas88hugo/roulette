using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using RouletteAPI.Helpers;
using AutoMapper;

namespace RouletteAPI.Controllers
{
  [Authorize]
  [Route("api/v1/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;
    private readonly AppSettings _appSettings;
    private IMapper _mapper;

    public UsersController(
      IUserRepository userRepository,
      IMapper mapper,
      IOptions<AppSettings> appSettings
    )
    {
      _userRepository = userRepository;
      _mapper = mapper;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
    {
      try
      {
        var user = await _userRepository.Authenticate(model.Username, model.Password);
        var tokenString = JwtHandler.CreateToken(user, _appSettings);
        return Ok(new
        {
          Id = user.Id,
          Username = user.Username,
          Token = tokenString
        });
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
      return await _userRepository.GetAllUsers();
    }

    [HttpGet("{id:length(24)}", Name = "GetUser")]
    public async Task<IActionResult> Get(string id)
    {
      User user = await _userRepository.GetUser(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody] RegisterModel model)
    {
      var user = _mapper.Map<User>(model);
      try
      {
        await _userRepository.CreateUser(user, model.Password);
        return Created("/", new { message = $"User with id {user.Id} has been created" });
      }
      catch (AppException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}