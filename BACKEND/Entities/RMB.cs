namespace BACKEND.Entities
{
    public class RMB
    {
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public double Result { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string ActivityLevel { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}