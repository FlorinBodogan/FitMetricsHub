namespace BACKEND.DTOs.Calculators.Rmb
{
    public class RmbCalculationRequest
    {
        public int Height { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ActivityLevel { get; set; }
        public int Weight { get; set; }
    }
}