namespace BACKEND.DTOs.Calculators.ArterialTension
{
    public class ArterialTensionDto
    {
        public int Sbp { get; set; }
        public int Dbp { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}