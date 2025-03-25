public class Spell
{
    public string Name { get; set; } = string.Empty;
    public List<string> Sections { get; set; } = new List<string>();

    public void AddSection(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            Sections.Add(content);
        }
    }

    public void PrintSection()
    {
        if (string.IsNullOrEmpty(Name))
        {
            Console.WriteLine("No spell information found.");
            return;
        }

        Console.WriteLine($"{Name}\n");

        if (Sections.Count == 0)
        {
            Console.WriteLine("No additional spell details found.");
            return;
        }

        foreach (var section in Sections)
        {
            Console.WriteLine($"{section}\n");
        }
    }
}
