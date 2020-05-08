using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RouletteAPI.Interfaces;
using RouletteAPI.Models;
using RouletteAPI.Helpers;
using AutoMapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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
      var user = await _userRepository.Authenticate(model.Username, model.Password);

      if (user == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info and authentication token
      return Ok(new
      {
        Id = user.Id,
        Username = user.Username,
        Token = tokenString
      });
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
        // create user
        await _userRepository.CreateUser(user, model.Password);
        return CreatedAtRoute("/",
          new { id = user.Id.ToString() },
          new { message = $"User with id {user.Id} has been created" });
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
      // User newUser = await _userRepository.CreateUser(user);
      // if (newUser == null)
      // {
      //   return BadRequest();
      // }
      // return CreatedAtRoute("Get",
      //   new { id = user.Id.ToString() },
      //   new { message = $"User with id {user.Id} has been created" });
    }
  }
}