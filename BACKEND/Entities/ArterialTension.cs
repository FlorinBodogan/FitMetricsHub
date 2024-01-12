namespace BACKEND.Entities
{
    public class ArterialTension
    {
        public string Id { get; set; }
        public int Sbp { get; set; }
        public int Dbp { get; set; }
        public string Result { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }
    }
}