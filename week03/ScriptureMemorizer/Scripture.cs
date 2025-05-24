using System;
using System.Collections.Generic;
using System.Linq;

class Scripture
{
    public ScriptureReference Reference { get; }
    private List<Word> words;
    private HashSet<int> hiddenIndexes;
    private Random random;

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        words = text.Split(' ').Select(word => new Word(word)).ToList();
        hiddenIndexes = new HashSet<int>();
        random = new Random();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{Reference}\n");

        foreach (var word in words)
            Console.Write(word + " ");

        Console.WriteLine("\n");
    }

    public void HideWords(int count)
    {
        var availableIndexes = words.Select((word, index) => index).Where(i => !hiddenIndexes.Contains(i)).ToList();

        if (availableIndexes.Count == 0) return;

        while (count > 0 && availableIndexes.Count > 0)
        {
            int index = availableIndexes[random.Next(availableIndexes.Count)];
            words[index].Hide();
            hiddenIndexes.Add(index);
            availableIndexes.Remove(index);
            count--;
        }
    }

    public bool AllWordsHidden() => hiddenIndexes.Count == words.Count;
}