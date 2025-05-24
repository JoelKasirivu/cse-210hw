using System;
using System.Collections.Generic;
using System.IO;
/*
Enhancements beyond requirements:
1. Loads scriptures from a file for added flexibility.
2. Random scripture selection ensures a fresh challenge each session.
3. Supports multi-verse scriptures using advanced constructors.
4. Advanced word hiding replaces words with matching-length underscores.
5. Ensures only non-hidden words are selected for progressive hiding.
*/
class Program
{
    static void Main()
    {
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Ensure the file contains valid scripture entries.");
            return;
        }

        Scripture selectedScripture = scriptures[new Random().Next(scriptures.Count)];
        selectedScripture.Display();

        Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");

        while (!selectedScripture.AllWordsHidden())
        {
            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "quit") break;

            selectedScripture.HideWords(2); // Hide 2 words at a time
            selectedScripture.Display();
        }

        Console.WriteLine("All words hidden! Try to recall the scripture from memory.");
    }

    static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();
        if (!File.Exists(filePath)) return scriptures;

        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split('|');
            if (parts.Length >= 2)
            {
                string referenceText = parts[0];
                string scriptureText = parts[1];

                string[] referenceParts = referenceText.Split(' ');
                if (referenceParts.Length >= 2)
                {
                    string book = referenceParts[0];
                    string chapterVerse = referenceParts[1];
                    string[] verseRange = chapterVerse.Split(':');

                    if (verseRange.Length == 2)
                    {
                        string chapter = verseRange[0];
                        string[] verseParts = verseRange[1].Split('-');

                        ScriptureReference reference = verseParts.Length == 1
                            ? new ScriptureReference(book, chapter, verseParts[0])
                            : new ScriptureReference(book, chapter, verseParts[0], verseParts[1]);

                        scriptures.Add(new Scripture(reference, scriptureText));
                    }
                }
            }
        }
        return scriptures;
    }
}