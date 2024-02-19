using BACKEND.DTOs.Calculators.Rmb;

namespace BACKEND.Interfaces
{
    public interface IRmbRepository
    {
        double CalculateRmb(string gender, int height, int weight, int age);
        double CalculateActivityFactor(string activityLevel);
        double CalculateRmbWithActivityLevel(string gender, int height, int weight, int age, string activityLevel);
        Task<RmbDto> CalculateRmbAsync(string gender, int height, int weight, int age, string activityLevel, string userId);
        Task<List<RmbDto>> GetRmbResultsForUserAsync(string userId);
        Task<int> DeleteUserRmbs(string userId);
    }
}