using System.CommandLine;

namespace MKDIRagons
{
    internal class CommandHandler
    {
        private readonly SpellScraper _scraper = new SpellScraper();

        public static RootCommand BuildRootCommand()
        {
            var rootCommand = new RootCommand("=== D&D Character Management CLI ===");

            // Character super command. Acts as a menu for the other character CRUD commands
            var characterCommand = new Command("--character", "Opens character management menu");
            rootCommand.Add(characterCommand);

            // "New" subcommand for building a new character
            var newCharacterCommand = new Command("--new", "Build a new 5e character");
            newCharacterCommand.SetHandler(() =>
            {
                //TODO: Add logic for building a new character
            });
            characterCommand.AddCommand(newCharacterCommand);


            // "Edit" subcommand for building a new character
            var editCharacterCommand = new Command("--edit", "Edit a 5e character");
            editCharacterCommand.SetHandler(() =>
            {
                //TODO: Add logic for building a new character
            });
            characterCommand.AddCommand(editCharacterCommand);


            // TODO: Continue adding commands, implement logic later

            return rootCommand;
        }
    }
}
