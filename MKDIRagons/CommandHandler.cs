using System.CommandLine;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace MKDIRagons
{
    internal class CommandHandler
    {
        private readonly SpellScraper _scraper = new SpellScraper();

        public RootCommand BuildRootCommand()
        {
            var rootCommand = new RootCommand("=== D&D 5e Spell Scraper CLI ===");
            var spellOption = new Option<string>("--spell", "The spell to scrape") { IsRequired = true };
            rootCommand.AddOption(spellOption);

            rootCommand.SetHandler(async (string spell) =>
            {
                var spellObj = await _scraper.ScrapeSpellAsync(spell);
                spellObj.CheckAndPrintFields();
            }, spellOption);

            return rootCommand;
        }
    }
}
