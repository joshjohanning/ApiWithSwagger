using ApiWithSwagger.Data.Models;
using ApiWithSwagger.Dtos;
using AutoMapper;
using System.Collections.Generic;

namespace ApiWithSwagger.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
