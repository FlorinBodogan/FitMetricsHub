namespace BACKEND.Entities
{
    public class RMB
    {
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }
    }
}