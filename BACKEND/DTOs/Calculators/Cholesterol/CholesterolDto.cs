namespace BACKEND.DTOs.Calculators.Cholesterol
{
    public class CholesterolDto
    {
        public int Hdl { get; set; }
        public int Ldl { get; set; }
        public int Triglycerides { get; set; }
        public int Result { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}