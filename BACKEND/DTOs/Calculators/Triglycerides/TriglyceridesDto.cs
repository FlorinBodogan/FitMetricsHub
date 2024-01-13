namespace BACKEND.DTOs.Calculators.Triglycerides
{
    public class TriglyceridesDto
    {
        public int Hdl { get; set; }
        public int Ldl { get; set; }
        public int Cholesterol { get; set; }
        public int Result { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}