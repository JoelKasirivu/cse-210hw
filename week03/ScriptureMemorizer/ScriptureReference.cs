using System;

class ScriptureReference
{
    public string Book { get; }
    public string Chapter { get; }
    public string Verse { get; }
    public string EndVerse { get; }

    // Constructor for single verse reference
    public ScriptureReference(string book, string chapter, string verse)
    {
        Book = book;
        Chapter = chapter;
        Verse = verse;
        EndVerse = null;
    }

    // Constructor for multi-verse reference
    public ScriptureReference(string book, string chapter, string verse, string endVerse)
    {
        Book = book;
        Chapter = chapter;
        Verse = verse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {Chapter}:{Verse}" : $"{Book} {Chapter}:{Verse}-{EndVerse}";
    }
}