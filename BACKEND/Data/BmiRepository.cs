using AutoMapper;
using BACKEND.DTOs.Calculators;
using BACKEND.DTOs.Calculators.Bmi;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class BmiRepository : IBmiRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public BmiRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<BmiDto> CalculateBmiAsync(int height, int weight, string userId)
        {
            var result = CalculateBmi(height, weight);

            var category = CategoryBmi(result);

            var bmiDto = new BmiDto
            {
                Result = result,
                Height = height,
                Weight = weight,
                Category = category,
                UserId = userId
            };

            BMI bmiEntity = this.mapper.Map<BMI>(bmiDto);
            bmiEntity.Id = Guid.NewGuid().ToString();
            this.context.BMIs.Add(bmiEntity);
            await this.context.SaveChangesAsync();

            return bmiDto;
        }

        public double CalculateBmi(int height, int weight)
        {

            double heightInMeters = height / 100.0;
            double bmi = weight / (heightInMeters * heightInMeters);

            return bmi;
        }

        public string CategoryBmi(double result)
        {
            string category = null;

            if (result < 18.5)
            {
                category = "Underweight";
            }
            else if (18.5 <= result && result < 25)
            {
                category = "Normal";
            }
            else if (25 <= result && result < 30)
            {
                category = "Overweight";
            }
            else if (result >= 30)
            {
                category = "Obese";
            }

            return category;
        }

        public async Task<List<BmiDto>> GetBmiResultsForUserAsync(string userId)
        {
            var bmiEntities = await this.context.BMIs.Where(b => b.UserId == userId).ToListAsync();
            return mapper.Map<List<BmiDto>>(bmiEntities);
        }

        public async Task<BmiCategoryStatsDto> GetBmiCategoryStatsAsync()
        {
            var latestBmiEntities = await Task.Run(() =>
                this.context.BMIs
                    .GroupBy(b => b.UserId)
                    .Select(g => g.OrderByDescending(b => b.Created).FirstOrDefault())
                    .ToList()
            );

            var categoryStats = new BmiCategoryStatsDto();

            foreach (var bmiEntity in latestBmiEntities)
            {
                if (bmiEntity != null)
                {
                    switch (bmiEntity.Category)
                    {
                        case "Normal":
                            categoryStats.NormalCount++;
                            break;
                        case "Underweight":
                            categoryStats.UnderweightCount++;
                            break;
                        case "Overweight":
                            categoryStats.OverweightCount++;
                            break;
                        case "Obese":
                            categoryStats.ObeseCount++;
                            break;
                    }
                }
            }

            return categoryStats;
        }
    }
}