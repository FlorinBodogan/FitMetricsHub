namespace BACKEND.DTOs.Calculators
{
    public class BmiDto
    {
        public double Result { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}