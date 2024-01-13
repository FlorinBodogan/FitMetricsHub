using AutoMapper;
using BACKEND.DTOs.Calculators.Triglycerides;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class TriglyceridesRepository : ITriglyceridesRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public TriglyceridesRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int CalculateTriglycerides(int hdl, int ldl, int cholesterol)
        {

            var triglycerides = 5 * (cholesterol - hdl - ldl);

            return triglycerides;
        }

        public string CheckCholesterolCategory(int triglycerides)
        {
            string category = "";

            if (triglycerides <= 150)
            {
                category = "Normal";
            }
            else if (151 <= triglycerides && triglycerides <= 199)
            {
                category = "Borderline High";
            }
            else if (200 <= triglycerides && triglycerides <= 500)
            {
                category = "High";
            }
            else if (500 <= triglycerides)
            {
                category = "Very High";
            }

            return category;
        }

        public async Task<TriglyceridesDto> CalculateCholesterolAsync(int hdl, int ldl, int cholesterol, string userId)
        {
            var result = CalculateTriglycerides(hdl, ldl, cholesterol);
            var category = CheckCholesterolCategory(result);

            var triglyceridesDto = new TriglyceridesDto
            {
                Hdl = hdl,
                Ldl = ldl,
                Cholesterol = cholesterol,
                Result = result,
                Category = category,
                UserId = userId
            };

            Triglycerides triEntity = this.mapper.Map<Triglycerides>(triglyceridesDto);
            triEntity.Id = Guid.NewGuid().ToString();
            this.context.Triglycerides.Add(triEntity);
            await this.context.SaveChangesAsync();

            return triglyceridesDto;
        }

        public async Task<List<TriglyceridesDto>> GetTriglyceridesResultsForUserAsync(string userId)
        {
            var triglyceridesEntities = await context.Triglycerides.Where(t => t.UserId == userId).ToListAsync();
            return mapper.Map<List<TriglyceridesDto>>(triglyceridesEntities);
        }

        public async Task<TriglyceridesCategoryStatsDto> GetTriglyceridesCategoryStatsAsync()
        {
            var latestTriglyceridesEntities = await Task.Run(() =>
                this.context.Triglycerides
                    .GroupBy(t => t.UserId)
                    .Select(g => g.OrderByDescending(t => t.Created).FirstOrDefault())
                    .ToList()
            );

            var categoryStats = new TriglyceridesCategoryStatsDto();

            foreach (var triglyceridesEntity in latestTriglyceridesEntities)
            {
                if (triglyceridesEntity != null)
                {
                    switch (triglyceridesEntity.Category)
                    {
                        case "Normal":
                            categoryStats.NormalCount++;
                            break;
                        case "Borderline High":
                            categoryStats.BorderlineHighCount++;
                            break;
                        case "High":
                            categoryStats.HighCount++;
                            break;
                        case "Very High":
                            categoryStats.VeryHighCount++;
                            break;
                    }
                }
            }

            return categoryStats;
        }

    }
}