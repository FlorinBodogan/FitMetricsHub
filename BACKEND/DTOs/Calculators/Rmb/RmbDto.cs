namespace BACKEND.DTOs.Calculators.Rmb
{
    public class RmbDto
    {
        public double Result { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ActivityLevel { get; set; }
        public int Weight { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}