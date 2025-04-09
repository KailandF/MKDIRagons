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

            // "New" Character subcommand for building a new character
            var newCharacterCommand = new Command("--new", "Build a new 5e character");
            newCharacterCommand.SetHandler(() =>
            {
                //TODO: Add logic for building a new character
            });
            characterCommand.AddCommand(newCharacterCommand);

            // "Print" (Read) Character subcommand for displaying character info
            var printCharacterCommand = new Command("--print", "Print the character information without sheet formatting");
            printCharacterCommand.SetHandler(() =>
            {
                // TODO: Add logic for printing character info, including stats and inventory
            });
            characterCommand.AddCommand(printCharacterCommand);

            // "Sheet" Character subcommand to print a Character Sheet format of stats
            var sheetCharacterCommand = new Command("--sheet", "Print the character sheet properly formatted");
            sheetCharacterCommand.SetHandler(() =>
            {
                // TODO: Add logic to print the formatted sheet
            });
            characterCommand.AddCommand(sheetCharacterCommand);

            // "Delete" Character subcommand to delete a character from the database
            var deleteCharacterCommand = new Command("--delete", "Delete the inputted character");
            deleteCharacterCommand.SetHandler(() =>
            {
                // TODO: Add logic for deleting a character based on a given input (char name)
            });
            characterCommand.AddCommand(deleteCharacterCommand);

            ////////// EDIT Character Commands //////////

            // "Edit" Character subcommand for building a new character
            var editCharacterCommand = new Command("--edit", "Edit a 5e character");
            editCharacterCommand.SetHandler(() =>
            {
                //TODO: Add logic for building a new character
            });
            characterCommand.AddCommand(editCharacterCommand);

            // "Level Up" Edit subcommand for assisting in character level up
            var levelupEditCommand = new Command("--levelup", "Level up character");
            levelupEditCommand.SetHandler(() =>
            {
                // TODO: Add logic for level up (may require manual entry, or level up table)
            });
            editCharacterCommand.AddCommand(levelupEditCommand);

            // "Stats" Edit subcommand for editing individual stats
            var statsEditCommand = new Command("--stats", "Edit a characters individual stats");
            statsEditCommand.SetHandler(() =>
            {
                // TODO: Add logic for editing character stats from db
            });
            editCharacterCommand.AddCommand(statsEditCommand);

            ////////// INVENTORY Edit subcommands //////////

            // "Inventory" Edit subcommand for managing items to a character's inventory
            var inventoryEditCommand = new Command("--inventory", "Opens inventory management menu");
            inventoryEditCommand.SetHandler(() =>
            {
                // TODO: Add logic for inventory menu command for subcommands (requires inv DbHandler)
            });
            editCharacterCommand.AddCommand(inventoryEditCommand);

            // "Add" Inventory subcommand for adding items to a character's inventory
            var addInventoryCommand = new Command("--add", "Add an item to a character's inventory");
            addInventoryCommand.SetHandler(() =>
            {
                // TODO: Add logic for adding item to character inventory (db add)
            });
            inventoryEditCommand.AddCommand(addInventoryCommand);

            // "Remove" Inventory subcommand to remove items from a character's inventory
            var removeInventoryCommand = new Command("--remove", "Remove an item from a character's inventory");
            removeInventoryCommand.SetHandler(() =>
            {
                // TODO: Add logic to remove item from character's inventory (db delete)
            });
            inventoryEditCommand.AddCommand(removeInventoryCommand);

            // "List" Inventory subcommand to list out the contents of a character's inventory
            var listInventoryCommand = new Command("--list", "List the contents of a character's inventory");
            listInventoryCommand.SetHandler(() =>
            {
                // TODO: Add logic to print out the character's inventory, including item descriptions
            });
            inventoryEditCommand.AddCommand(listInventoryCommand);

            ////////// SPELL Edit subcommands //////////

            // "Spells" Edit subcommand for managing a character's spell list
            var spellEditCommand = new Command("--spells", "Opens spell management menu");
            spellEditCommand.SetHandler(() =>
            {
                // TODO: Add logic for spell menu command for spell subcommands (need SpellScrape)
            });
            editCharacterCommand.AddCommand(spellEditCommand);

            // "Add" Spell subcommand for adding spells to character spell list
            var addSpellCommand = new Command("--add", "Add spells to a character's spell sheet");
            addSpellCommand.SetHandler(() =>
            {
                // TODO: Add logic to add spell to character spell list using SpellScraper and db
            });
            spellEditCommand.AddCommand(addSpellCommand);

            // "Remove" Spell subcommand for removing spell from character spell list
            var removeSpellCommand = new Command("--remove", "Remove a spell from a character's spell list");
            removeInventoryCommand.SetHandler(() =>
            {
                // TODO: Add logic to remove spells from character's spell list (db delete)
            });
            spellEditCommand.AddCommand(removeSpellCommand);

            // "List" Spell subcommand to list out the contents of a character's inventory
            var listSpellCommand = new Command("--list", "List the contents of a character's spell list");
            listInventoryCommand.SetHandler(() =>
            {
                // TODO: Add logic to print out the character's spell list, sorted by level
            });
            inventoryEditCommand.AddCommand(listSpellCommand);

            ////////// SPELLBOOK super command //////////

            // "Spellbook" super command for searching and printing a spell from the internet
            var spellbookCommand = new Command("--spellbook", "Search the internet for a spell");
            spellbookCommand.SetHandler(() =>
            {
                // TODO: Add logic to scrape spells (use SpellScrape)
            });
            rootCommand.AddCommand(spellbookCommand);

            ////////// ITEM super command //////////

            // "Item" super command for searching and printing an item from the internet
            var itemCommand = new Command("--item", "Search the internet for a standard item");
            itemCommand.SetHandler(() =>
            {
                // TODO: Add logic to scrape items (requires ItemScrape, not implemented yet)
            });
            rootCommand.AddCommand(itemCommand);

            return rootCommand;
        }
    }
}
