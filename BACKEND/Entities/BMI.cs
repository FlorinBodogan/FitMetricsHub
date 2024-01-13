namespace BACKEND.Entities
{
    public class BMI
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public double Result { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }
    }
}