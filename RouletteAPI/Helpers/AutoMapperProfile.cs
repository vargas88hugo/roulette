using AutoMapper;
using RouletteAPI.Models;

namespace RouletteAPI.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User, UserModel>();
      CreateMap<RegisterModel, User>();
      // CreateMap<UpdateModel, User>();
    }
  }
}