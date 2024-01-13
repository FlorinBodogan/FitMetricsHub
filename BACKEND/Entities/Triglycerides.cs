namespace BACKEND.Entities
{
    public class Triglycerides
    {
        public string Id { get; set; }
        public int Hdl { get; set; }
        public int Ldl { get; set; }
        public int Cholesterol { get; set; }
        public int Result { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public string UserId { get; set; }
    }
}