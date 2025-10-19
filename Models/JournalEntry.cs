namespace MindEaseApp.Models
{
    public class JournalEntry
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Mood { get; set; } = "";
        public string Reflection { get; set; } = "";
        public string Title { get; set; } = "";         // New: title for reflection
        public string Feeling { get; set; } = "";       // New: a short feeling/emotion
        public string Quote { get; set; } = "";
    }
}
