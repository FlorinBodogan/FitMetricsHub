using AutoMapper;
using BACKEND.DTOs.Account;
using BACKEND.DTOs.Calculators;
using BACKEND.DTOs.Calculators.ArterialTension;
using BACKEND.DTOs.Calculators.Cholesterol;
using BACKEND.DTOs.Calculators.Rmb;
using BACKEND.DTOs.Calculators.Triglycerides;
using BACKEND.Entities;

namespace BACKEND.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping for User registration
            CreateMap<RegisterDto, User>();

            // Mapping for BMI
            CreateMap<BmiDto, BMI>();
            CreateMap<BMI, BmiDto>();

            // Mapping for RMB
            CreateMap<RmbDto, RMB>();
            CreateMap<RMB, RmbDto>();

            // Mapping for Arterial Tension
            CreateMap<ArterialTensionDto, ArterialTension>();
            CreateMap<ArterialTension, ArterialTensionDto>();

            // Mapping for Cholesterol
            CreateMap<CholesterolDto, Cholesterol>();
            CreateMap<Cholesterol, CholesterolDto>();

            // Mapping for Triglycerides
            CreateMap<TriglyceridesDto, Triglycerides>();
            CreateMap<Triglycerides, TriglyceridesDto>();
        }
    }
}