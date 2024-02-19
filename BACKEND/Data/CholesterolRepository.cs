using AutoMapper;
using BACKEND.DTOs.Calculators.Cholesterol;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class CholesterolRepository : ICholesterolRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CholesterolRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int CalculateCholesterol(int hdl, int ldl, int triglycerides)
        {

            var colesterol = hdl + ldl + 0.2 * triglycerides;

            return (int)colesterol;
        }

        public string CheckCholesterolCategory(int cholesterol)
        {
            string category = "";

            if (cholesterol <= 200)
            {
                category = "Normal";
            }
            else if (200 <= cholesterol && cholesterol <= 239)
            {
                category = "Borderline High";
            }
            else if (240 <= cholesterol)
            {
                category = "High";
            }

            return category;
        }

        public async Task<CholesterolDto> CalculateCholesterolAsync(int hdl, int ldl, int triglycerides, string userId)
        {
            var result = CalculateCholesterol(hdl, ldl, triglycerides);
            var category = CheckCholesterolCategory(result);

            var cholesterolDto = new CholesterolDto
            {
                Hdl = hdl,
                Ldl = ldl,
                Triglycerides = triglycerides,
                Result = result,
                Category = category,
                UserId = userId
            };

            Cholesterol colEntity = this.mapper.Map<Cholesterol>(cholesterolDto);
            colEntity.Id = Guid.NewGuid().ToString();
            this.context.Cholesterols.Add(colEntity);
            await this.context.SaveChangesAsync();

            return cholesterolDto;
        }

        public async Task<List<CholesterolDto>> GetCholesterolResultsForUserAsync(string userId)
        {
            var cholesterolEntities = await this.context.Cholesterols.Where(c => c.UserId == userId).ToListAsync();
            return mapper.Map<List<CholesterolDto>>(cholesterolEntities);
        }

        public async Task<CholesterolCategoryStatsDto> GetCholesterolCategoryStatsAsync()
        {
            var latestCholesterolEntities = await Task.Run(() =>
                this.context.Cholesterols
                    .GroupBy(c => c.UserId)
                    .Select(g => g.OrderByDescending(c => c.Created).FirstOrDefault())
                    .ToListAsync()
            );

            var categoryStats = new CholesterolCategoryStatsDto();

            foreach (var cholesterolEntity in latestCholesterolEntities)
            {
                if (cholesterolEntity != null)
                {
                    switch (cholesterolEntity.Category)
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
                    }
                }
            }

            return categoryStats;
        }

        public async Task<int> DeleteUserCholesterols(string userId)
        {
            var userCholesterols = this.context.Cholesterols.Where(u => u.UserId == userId);

            this.context.RemoveRange(userCholesterols);

            return await this.context.SaveChangesAsync();
        }
    }
}