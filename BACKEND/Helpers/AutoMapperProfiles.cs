using AutoMapper;
using BACKEND.DTOs.Account;
using BACKEND.Entities;

namespace BACKEND.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}