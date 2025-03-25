namespace MKDIRagons
{
    public class Spell
    {
        public string Name { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string School { get; set; } = string.Empty;
        public string CastingTime { get; set; } = string.Empty;
        public string Range { get; set; } = string.Empty;
        public string Components { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Upcast { get; set; } = string.Empty;

        public Spell()
        {
        }
        public Spell(
            string name,
            string source,
            string level,
            string school,
            string castingtime,
            string range,
            string components,
            string duration,
            string description,
            string upcast)
        {
            Name = name;
            Source = source;
            Level = level;
            School = school;
            CastingTime = castingtime;
            Range = range;
            Components = components;
            Duration = duration;
            Description = description;
            Upcast = upcast;
        }


        public void AddFieldData(string fieldName, string? newFieldData)
        {
            // New data for the field is either itself or N/A if not provided
            newFieldData = newFieldData ?? "N/A";

            // Find field and set it to the new data
            switch (fieldName.ToLower())
            {
                case "name":
                    Name = newFieldData;
                    break;

                case "source":
                    Source = newFieldData;
                    break;

                case "level":
                    Level = newFieldData;
                    break;

                case "school":
                    School = newFieldData;
                    break;

                case "castingtime":
                    CastingTime = newFieldData;
                    break;

                case "range":
                    Range = newFieldData;
                    break;

                case "components":
                    Components = newFieldData;
                    break;

                case "duration":
                    Duration = newFieldData;
                    break;

                case "description":
                    Description = newFieldData;
                    break;

                case "upcast":
                    Upcast = newFieldData;
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
                // Handle null case, null treated as "N/A"
                check = "N/A";
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
                { "Source", Source },
                { "Level", Level },
                { "School", School },
                { "CastingTime", CastingTime },
                { "Range", Range },
                { "Components", Components },
                { "Duration", Duration },
                { "Description", Description },
                { "Upcast", Upcast }
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
