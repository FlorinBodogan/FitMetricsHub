namespace BACKEND.Entities
{
    public class Cholesterol
    {
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public int Hdl { get; set; }
        public int Ldl { get; set; }
        public int Triglycerides { get; set; }
        public string Result { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}