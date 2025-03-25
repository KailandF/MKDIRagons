using System.CommandLine;

namespace MKDIRagons
{
    internal class CommandHandler
    {
        private readonly SpellScraper _scraper = new SpellScraper();

        public RootCommand BuildRootCommand()
        {
            var spellOption = new Option<string>("--spell", "The spell to scrape") { IsRequired = true };
            var rootCommand = new RootCommand("D&D 5e Spell Scraper CLI");
            rootCommand.AddOption(spellOption);

            rootCommand.SetHandler(async (string spell) =>
            {
                var spellObj = await _scraper.ScrapeSpellAsync(spell);
                spellObj.PrintSection();
            }, spellOption);

            return rootCommand;
        }
    }
}
