using System.Text;

class WordData
{
    public string Word { get; set; }
    public int Count { get; set; }

    public WordData(string word)
    {
        Word = word;
        Count = 1;
    }
}

class Program
{
    static void Main()
    {
        string book = File.ReadAllText("book.txt", Encoding.Default);
        List<string> words = book.Split(' ').ToList();

        List<WordData> result = new List<WordData>();

        for (int i = 0; i < words.Count; i++)
        {
            string word = words[i].ToLower().Trim();
            AddWord(ref result, word);
            Console.Title = $"Обработано: {Math.Round((double)i / words.Count * 100.0)} %\t Слова в списке: {result.Count}";
        }

        result = result.OrderByDescending(i => i.Count).ToList();

        for (int i = 0; i < 100; i++)
            Console.WriteLine($"{result[i].Word} ({result[i].Count})");

        List<string> list_save = new List<string>();
        foreach (WordData wd in result)
            list_save.Add($"{wd.Word} ({wd.Count})");
        File.WriteAllLines("result.txt", list_save);
    }

    private static void AddWord(ref List<WordData> list, string word)
    {
        word = word.Trim();
        if (word == String.Empty) return;
        bool found = false;
        foreach (WordData wd in list)
        {
            if (wd.Word == word)
            {
                found = true;
                wd.Count++;
                break;
            }
        }

        if (!found)
            list.Add(new WordData(word));
    }
}
