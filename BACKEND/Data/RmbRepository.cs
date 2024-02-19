using AutoMapper;
using BACKEND.DTOs.Calculators.Rmb;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class RmbRepository : IRmbRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public RmbRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public double CalculateActivityFactor(string activityLevel)
        {
            switch (activityLevel.ToLower())
            {
                case "veryLow":
                    return 1.2;
                case "low":
                    return 1.375;
                case "moderate":
                    return 1.55;
                case "high":
                    return 1.725;
                case "veryHigh":
                    return 1.9;
                default:
                    throw new ArgumentException("This value its not suitable.", nameof(activityLevel));
            }
        }

        public double CalculateRmb(string gender, int height, int weight, int age)
        {
            double rmb;

            if (gender.ToLower() == "male")
            {
                rmb = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
            }
            else
            {
                rmb = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
            }

            return rmb;
        }

        public double CalculateRmbWithActivityLevel(string gender, int height, int weight, int age, string activityLevel)
        {
            double rmb = CalculateRmb(gender, height, weight, age);
            double activityFactor = CalculateActivityFactor(activityLevel);

            return rmb * activityFactor;
        }

        public async Task<RmbDto> CalculateRmbAsync(string gender, int height, int weight, int age, string activityLevel, string userId)
        {
            var result = CalculateRmbWithActivityLevel(gender, height, weight, age, activityLevel);

            var rmbDto = new RmbDto
            {
                Result = result,
                Gender = gender,
                Height = height,
                Weight = weight,
                Age = age,
                ActivityLevel = activityLevel,
                UserId = userId
            };

            RMB rmbEntity = this.mapper.Map<RMB>(rmbDto);
            rmbEntity.Id = Guid.NewGuid().ToString();
            this.context.Add(rmbEntity);
            await this.context.SaveChangesAsync();

            return rmbDto;
        }

        public async Task<List<RmbDto>> GetRmbResultsForUserAsync(string userId)
        {
            var rmbEntities = await context.RMBs.Where(r => r.UserId == userId).ToListAsync();
            return mapper.Map<List<RmbDto>>(rmbEntities);
        }

        public async Task<int> DeleteUserRmbs(string userId)
        {
            var userRmbs = this.context.RMBs.Where(u => u.UserId == userId).ToList();

            foreach (var rmb in userRmbs.ToList())
            {
                if (double.IsInfinity(rmb.Result))
                {
                    userRmbs.Remove(rmb);
                }
            }

            this.context.RemoveRange(userRmbs);

            return await this.context.SaveChangesAsync();
        }
    }
}