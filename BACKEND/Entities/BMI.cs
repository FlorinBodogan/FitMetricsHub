namespace BACKEND.Entities
{
    public class BMI
    {
        private double _result;

        public string Id { get; set; }
        public string Category { get; set; }
        public double Result
        {
            get => _result;
            set => _result = Math.Round(value, 2);
        }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }

        public BMI()
        {
            Result = Math.Round(Result, 2);
        }
    }
}