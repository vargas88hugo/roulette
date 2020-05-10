using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Interfaces;
using RouletteApi.Models.Entities;

namespace RouletteApi.Controllers
{
  [Authorize]
  [Route("api/v2/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService) =>
      _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() =>
      Ok(await _userService.GetAllUsers());

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetUser(string id) =>
      Ok(await _userService.GetUser(id));
  }
}