namespace BACKEND.DTOs.Calculators.Bmi
{
    public class BmiCategoryStatsDto
    {
        public int NormalCount { get; set; }
        public int UnderweightCount { get; set; }
        public int OverweightCount { get; set; }
        public int ObeseCount { get; set; }
    }
}