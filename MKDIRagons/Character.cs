namespace MKDIRagons
{
    internal class Character
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string CharacterClass { get; set; }
        public string Level { get; set; } = "10";
        public string Strength { get; set; } = "10";
        public string Dexterity { get; set; } = "10";
        public string Constitution { get; set; } = "10";
        public string Intelligence { get; set; } = "10";
        public string Wisdom { get; set; } = "10";
        public string Charisma { get; set; } = "10";

        public Character(
            string name, 
            string race, 
            string characterClass, 
            string level, 
            string str, 
            string dex, 
            string con, 
            string intel, 
            string wis, 
            string cha)
        {
            Name = name;
            Race = race;
            CharacterClass = characterClass;
            Level = level;
            Strength = str;
            Dexterity = dex;
            Constitution = con;
            Intelligence = intel;
            Wisdom = wis;
            Charisma = cha;
        }

        public void AddFieldData(string fieldName, string? newFieldData)
        {
            newFieldData = newFieldData ?? "10";

            // Find field and set it to the new data
            switch (fieldName.ToLower())
            {
                case "name":
                    Name = newFieldData!;
                    break;

                case "race":
                    Race = newFieldData;
                    break;

                case "characterclass":
                    CharacterClass = newFieldData;
                    break;

                case "level":
                    Level = newFieldData;
                    break;

                case "strength":
                    Strength = newFieldData;
                    break;

                case "dexterity":
                    Dexterity = newFieldData;
                    break;

                case "constitution":
                    Constitution = newFieldData;
                    break;

                case "intelligence":
                    Intelligence = newFieldData;
                    break;

                case "wisdom":
                    Wisdom = newFieldData;
                    break;

                case "charisma":
                    Charisma = newFieldData;
                    break;
            }
        }

        public void ReplaceField(string fieldName, string oldFieldData)
        {
            Console.WriteLine($"No spell {oldFieldData} found.");
            Console.WriteLine("Do you want to add this?");
            Console.WriteLine("Y / N");

            // Get new input
            string? check = Console.ReadLine();
            if (check == null)
            {
                // Handle null case, null treated as ""
                check = "";
            }
            check = check.ToUpper();

            while (true)
            {
                switch (check)
                {
                    case "Y":
                        Console.WriteLine("Please input the desired field");
                        string? newFieldData = Console.ReadLine();
                        AddFieldData(fieldName, newFieldData);
                        return;

                    case "N":
                        Console.WriteLine("No field added");
                        AddFieldData(fieldName, "N/A");
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please enter Y / N");
                        break;
                }
            }
        }

        public void CheckAndPrintFields(string? specificField = null)
        {
            var fields = new Dictionary<string, string>
            {
                { "Name", Name },
                { "Race", Race },
                { "CharacterClass", CharacterClass },
                { "Level", Level },
                { "Strength", Strength },
                { "Dexterity", Dexterity },
                { "Constitution", Constitution },
                { "Intelligence", Intelligence },
                { "Wisdom", Wisdom },
                { "Charisma", Charisma }
            };

            if (specificField != null)
            {
                // If a specific field is provided, only check and print that field
                if (fields.TryGetValue(specificField, out string? value))
                {
                    CheckAndPrintSingleField(specificField, value);
                }
                else
                {
                    Console.WriteLine($"Field {specificField} not found.");
                }
            }
            else
            {
                // If no specific field is provided, check and print all fields
                foreach (var field in fields)
                {
                    CheckAndPrintSingleField(field.Key, field.Value);
                }
            }
        }

        private void CheckAndPrintSingleField(string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                ReplaceField(fieldName, fieldValue);
            }
            Console.WriteLine($"{fieldName}: {fieldValue}\n");
        }
    }
}
