namespace MentalEase.Models
{
    public class ResponseModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Message { get; set; }
        public int MoodLevel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}