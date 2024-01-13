using BACKEND.DTOs.Calculators;
using BACKEND.DTOs.Calculators.Bmi;

namespace BACKEND.Interfaces
{
    public interface IBmiRepository
    {
        double CalculateBmi(int height, int weight);
        string CategoryBmi(double result);
        Task<BmiDto> CalculateBmiAsync(int height, int weight, string UserName);
        Task<List<BmiDto>> GetBmiResultsForUserAsync(string userId);
        Task<BmiCategoryStatsDto> GetBmiCategoryStatsAsync();
    }
}