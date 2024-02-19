using BACKEND.DTOs.Calculators.ArterialTension;

namespace BACKEND.Interfaces
{
    public interface IArterialTensionRepository
    {
        string CheckCategoryForAt(int sbp, int dbp);
        Task<ArterialTensionDto> CalculateArterialTension(int sbp, int dbp, string userId);
        Task<List<ArterialTensionDto>> GetArterialTensionResultsForUserAsync(string userId);
        Task<ArterialTensionCategoryStatsDto> GetArterialTensionCategoryStatsAsync();

        Task<int> DeleteUserATs(string userId);
    }
}