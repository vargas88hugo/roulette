using AutoMapper;
using RouletteApi.Models;
using RouletteApi.Models.Entities;

namespace RouletteApi.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User, UserModel>();
      CreateMap<RegisterModel, User>();
    }
  }
}