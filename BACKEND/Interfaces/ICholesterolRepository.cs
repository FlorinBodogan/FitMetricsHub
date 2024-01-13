using BACKEND.DTOs.Calculators.Cholesterol;

namespace BACKEND.Interfaces
{
    public interface ICholesterolRepository
    {
        string CheckCholesterolCategory(int cholesterol);
        int CalculateCholesterol(int hdl, int ldl, int triglycerides);
        Task<CholesterolDto> CalculateCholesterolAsync(int hdl, int ldl, int triglycerides, string userId);
        Task<List<CholesterolDto>> GetCholesterolResultsForUserAsync(string userId);
        Task<CholesterolCategoryStatsDto> GetCholesterolCategoryStatsAsync();
    }
}