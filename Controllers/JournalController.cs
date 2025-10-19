using Microsoft.AspNetCore.Mvc;
using MindEaseApp.Models;
using System.Text.Json;
using System.IO;

namespace MindEaseApp.Controllers
{
    public class JournalController : Controller
    {
        private static List<JournalEntry> entries = new List<JournalEntry>();
        private static readonly string filePath = "journalEntries.json"; // file to store entries

        // Dictionary mapping moods to relevant quotes
        private static readonly Dictionary<string, string[]> quotesByMood = new Dictionary<string, string[]>
        {
            { "😊 Happy", new[] { "Keep smiling, happiness suits you!", "Joy is contagious — spread it today!" } },
            { "😄 Excited", new[] { "Your excitement can light up the world!", "Channel your energy into something great today!" } },
            { "😐 Okay", new[] { "Every day is a fresh start.", "Take it slow and steady — progress counts!" } },
            { "😔 Sad", new[] { "Tough days don’t last forever.", "It’s okay to feel sad — tomorrow is a new chance!" } },
            { "😡 Angry", new[] { "Take a deep breath and find your calm.", "Let go of anger, focus on your growth." } },
            { "😨 Anxious", new[] { "You are stronger than your worries.", "One small step at a time — you’ve got this!" } },
            { "😴 Tired", new[] { "Rest is productive — recharge your energy.", "Even small naps can boost your day!" } },
            { "😇 Calm", new[] { "Peace begins with you.", "Stay calm and carry on — you’re doing great!" } },
            { "🤔 Thoughtful", new[] { "Reflection is the key to growth.", "Your thoughts shape your tomorrow." } }
        };

        // Load entries from file when the controller is first used
        static JournalController()
        {
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                entries = JsonSerializer.Deserialize<List<JournalEntry>>(json) ?? new List<JournalEntry>();
            }
        }

        // Save entries to JSON file
        private void SaveEntries()
        {
            var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, json);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(entries);
        }

        [HttpPost]
        public IActionResult Add(string mood, string feeling, string title, string reflection)
        {
            string quote = "Keep going!"; // default fallback quote

            // Pick a random quote based on the selected mood
            if (!string.IsNullOrEmpty(mood) && quotesByMood.ContainsKey(mood))
            {
                var random = new Random();
                var quotesForMood = quotesByMood[mood];
                quote = quotesForMood[random.Next(quotesForMood.Length)];
            }

            var entry = new JournalEntry
            {
                Mood = mood,
                Feeling = feeling,
                Title = title,
                Reflection = reflection,
                Quote = quote
            };

            entries.Add(entry);

            // Save entries to file
            SaveEntries();

            return RedirectToAction("Index");
        }
    }
}
