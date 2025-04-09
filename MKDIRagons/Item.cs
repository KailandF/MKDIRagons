namespace MKDIRagons
{
    public class Item
    {

        public string Name { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public Item() { }

        public Item(string name, string weight, string description, string source)
        {
            Name = name;
            Weight = weight;
            Description = description;
            Source = source;
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

                case "description":
                    Description = newFieldData;
                    break;

                case "source":
                    Source = newFieldData;
                    break;
            }
        }

        public void ReplaceField(string fieldName, string oldFieldData)
        {
            Console.WriteLine($"No item {oldFieldData} found.");
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
                { "Description", Description },
                { "Source", Source }
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
