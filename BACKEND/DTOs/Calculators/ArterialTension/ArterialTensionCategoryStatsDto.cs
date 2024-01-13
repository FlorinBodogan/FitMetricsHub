namespace BACKEND.DTOs.Calculators.ArterialTension
{
    public class ArterialTensionCategoryStatsDto
    {
        public int OptimalCount { get; set; }
        public int NormalCount { get; set; }
        public int ElevatedCount { get; set; }
        public int HypertensionStage1Count { get; set; }
        public int HypertensionStage2Count { get; set; }
        public int HypertensionStage3Count { get; set; }
        public int IsolatedSystolicHypertensionCount { get; set; }
        public int UndeterminedCount { get; set; }
    }
}