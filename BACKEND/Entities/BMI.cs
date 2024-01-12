namespace BACKEND.Entities
{
    public class BMI
    {
        public string Id { get; set; }
        public double Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }
    }
}