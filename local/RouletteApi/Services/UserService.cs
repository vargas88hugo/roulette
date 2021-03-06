using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteApi.Interfaces;
using RouletteApi.Models.Entities;

namespace RouletteApi.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) =>
      _userRepository = userRepository;

    public async Task<IEnumerable<User>> GetAllUsers() =>
      await _userRepository.GetAllUsers();

    public async Task<User> GetUser(string id)
    {
      var user = await _userRepository.GetUserById(id);
      if (user == null)
        throw new Exception($"User with id {id} not found");
      return user;
    }
  }
}