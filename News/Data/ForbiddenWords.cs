using Newtonsoft.Json;

namespace News.Data
{
    public static class ForbiddenWords
    {
        private static readonly Lazy<List<string>> _words = new(LoadWords);
        public static List<string> Words => _words.Value;
        private static readonly string _filePath = "forbidden-words.json";

        public static bool IsForbidden(string text)
        {
            return _words.Value.Any(text.Contains);
        }

        public static void AddWord(string word)
        {
            if (Words.Contains(word)) return;
            Words.Add(word);
            SaveWords();
        }

        public static void RemoveWord(string word)
        {
            if (!Words.Contains(word)) return;
            Words.Remove(word);
            SaveWords();
        }

        private static List<string> LoadWords()
        {
            if (!File.Exists(_filePath))
                _initWords();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();

        }

        private static void _initWords()
        {
            var words = new List<string>
            {
                "Exorcizamus",
                "omnis",
                "immundus",
                "spiritus",
                "satanica",
                "incursio",
                "infernalis",
                "adversarii",
                "legio",
                "congregatio",
                "secta",
                "diabolica"
                
                //Запрещены слова для изгнания демонов, призраков и ангелов
                //https://pbs.twimg.com/media/CRKP4K0WIAE2Dko.jpg
            };
            var jsonWords = JsonConvert.SerializeObject(words);
            File.WriteAllText(_filePath,jsonWords);
        }

        private static void SaveWords()
        {
            var json = JsonConvert.SerializeObject(Words, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }

}
