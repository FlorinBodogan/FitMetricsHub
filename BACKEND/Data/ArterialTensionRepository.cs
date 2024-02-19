using AutoMapper;
using BACKEND.DTOs.Calculators.ArterialTension;
using BACKEND.Entities;
using BACKEND.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class ArterialTensionRepository : IArterialTensionRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ArterialTensionRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ArterialTensionDto> CalculateArterialTension(int sbp, int dbp, string userId)
        {
            var result = CheckCategoryForAt(sbp, dbp);

            var arterialTensionDto = new ArterialTensionDto
            {
                Sbp = sbp,
                Dbp = dbp,
                Category = result,
                UserId = userId
            };

            ArterialTension atEntity = this.mapper.Map<ArterialTension>(arterialTensionDto);
            atEntity.Id = Guid.NewGuid().ToString();
            this.context.ArterialTensions.Add(atEntity);
            await this.context.SaveChangesAsync();

            return arterialTensionDto;
        }

        public string CheckCategoryForAt(int sbp, int dbp)
        {
            string category;

            if (sbp <= 120 && dbp <= 80)
            {
                category = "Optimal";
            }
            else if (sbp >= 120 && sbp <= 129 && dbp >= 80 && dbp <= 84)
            {
                category = "Normal";
            }
            else if (sbp >= 130 && sbp <= 139 && dbp >= 85 && dbp <= 89)
            {
                category = "Elevated";
            }
            else if (sbp >= 140 && sbp <= 159 && dbp >= 90 && dbp <= 99)
            {
                category = "Hypertension Stage 1";
            }
            else if (sbp >= 160 && sbp <= 179 && dbp >= 100 && dbp <= 109)
            {
                category = "Hypertension Stage 2";
            }
            else if (sbp >= 180 && dbp >= 110)
            {
                category = "Hypertension Stage 3";
            }
            else if (sbp >= 140 && dbp < 90)
            {
                category = "Isolated Systolic Hypertension";
            }
            else
            {
                category = "Undetermined";
            }

            return category;
        }

        public async Task<List<ArterialTensionDto>> GetArterialTensionResultsForUserAsync(string userId)
        {
            var arterialTensionEntities = await this.context.ArterialTensions.Where(at => at.UserId == userId).ToListAsync();
            return mapper.Map<List<ArterialTensionDto>>(arterialTensionEntities);
        }

        public async Task<ArterialTensionCategoryStatsDto> GetArterialTensionCategoryStatsAsync()
        {
            var latestArterialTensionEntities = await Task.Run(() =>
                this.context.ArterialTensions
                    .GroupBy(at => at.UserId)
                    .Select(g => g.OrderByDescending(at => at.Created).FirstOrDefault())
                    .ToList()
            );

            var categoryStats = new ArterialTensionCategoryStatsDto();

            foreach (var arterialTensionEntity in latestArterialTensionEntities)
            {
                if (arterialTensionEntity != null)
                {
                    switch (arterialTensionEntity.Category)
                    {
                        case "Optimal":
                            categoryStats.OptimalCount++;
                            break;
                        case "Normal":
                            categoryStats.NormalCount++;
                            break;
                        case "Elevated":
                            categoryStats.ElevatedCount++;
                            break;
                        case "Hypertension Stage 1":
                            categoryStats.HypertensionStage1Count++;
                            break;
                        case "Hypertension Stage 2":
                            categoryStats.HypertensionStage2Count++;
                            break;
                        case "Hypertension Stage 3":
                            categoryStats.HypertensionStage3Count++;
                            break;
                        case "Isolated Systolic Hypertension":
                            categoryStats.IsolatedSystolicHypertensionCount++;
                            break;
                        case "Undetermined":
                            categoryStats.UndeterminedCount++;
                            break;
                    }
                }
            }

            return categoryStats;
        }

        public async Task<int> DeleteUserATs(string userId)
        {
            var userATs = this.context.ArterialTensions.Where(u => u.UserId == userId);

            this.context.RemoveRange(userATs);

            return await this.context.SaveChangesAsync();
        }
    }
}