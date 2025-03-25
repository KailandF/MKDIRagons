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

        public void FieldChecker(string fieldName, string oldFieldData)
        {
            Console.WriteLine($"No spell {oldFieldData} found.");
            Console.WriteLine("Do you want to add this?");
            Console.WriteLine("Y / N");

            // Get new input
            string? check = Console.ReadLine();
            if (check == null)
            {
                // Handle null case, null treated as ""
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
        public void CheckAndPrintField(string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                FieldChecker(fieldName, fieldValue);
            }
            Console.WriteLine($"{fieldValue}\n");
        }
    }
}
