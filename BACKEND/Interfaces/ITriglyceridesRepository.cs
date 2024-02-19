using BACKEND.DTOs.Calculators.Triglycerides;

namespace BACKEND.Interfaces
{
    public interface ITriglyceridesRepository
    {
        int CalculateTriglycerides(int hdl, int ldl, int cholesterol);
        string CheckCholesterolCategory(int triglycerides);
        Task<TriglyceridesDto> CalculateCholesterolAsync(int hdl, int ldl, int cholesterol, string userid);
        Task<List<TriglyceridesDto>> GetTriglyceridesResultsForUserAsync(string userId);
        Task<TriglyceridesCategoryStatsDto> GetTriglyceridesCategoryStatsAsync();
        Task<int> DeleteUserTriglycerides(string userId);
    }
}