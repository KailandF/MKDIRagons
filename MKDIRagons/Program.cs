using System.CommandLine;

namespace MKDIRagons
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            if (args.Length > 0)
                return await RunCommandLine(args);

            return await RunInteractiveMode();
        }

        static async Task<int> RunCommandLine(string[] args)
        {
            var handler = new CommandHandler();
            var rootCommand = handler.BuildRootCommand();
            return await rootCommand.InvokeAsync(args);
        }

        static async Task<int> RunInteractiveMode()
        {
            var scraper = new SpellScraper();

            Console.WriteLine("=== D&D 5e Spell Scraper Interactive Mode ===");
            while (true)
            {
                Console.Write("\n> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                if (input.StartsWith("--spell"))
                {
                    string spellName = input.Substring("--spell".Length).Trim();
                    var spellObj = await scraper.ScrapeSpellAsync(spellName);
                    spellObj.PrintSection();
                }
                else if (input == "--clear")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }

            return 0;
        }
    }
}
