using System.Data.SQLite;
using System.Diagnostics;

namespace MKDIRagons
{
    internal class SpellDbHandler
    {
        private readonly SQLiteConnection _connection;
        public SpellDbHandler(SQLiteConnection connection)
        {
            _connection = connection;
            CreateSpell();
        }

        public void CreateSpell()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Spells (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL DEFAULT 'N/A',
                    Source TEXT NOT NULL DEFAULT 'N/A',
                    Level TEXT NOT NULL DEFAULT 'N/A',
                    School TEXT NOT NULL DEFAULT 'N/A',
                    CastingTime TEXT NOT NULL DEFAULT 'N/A',
                    Range TEXT NOT NULL DEFAULT 'N/A',
                    Components TEXT NOT NULL DEFAULT 'N/A',
                    Duration TEXT NOT NULL DEFAULT 'N/A',
                    Description TEXT NOT NULL DEFAULT 'N/A',
                    Upcast TEXT NOT NULL DEFAULT 'N/A'
                );";

            using var command = new SQLiteCommand(createTableQuery, _connection);
            command.ExecuteNonQuery();
        }

        public void InsertSpell(Spell spell)
        {
            string insertQuery = @"
                INSERT INTO Spells
                    (Name, Source, Level, School, CastingTime, Range, Components, Duration, Description, Upcast)
                VALUES 
                    (@name, @source, @level, @school, @castingtime, @range, @components, @duration, @description, @upcast);";

            using var command = new SQLiteCommand(insertQuery, _connection);
            command.Parameters.AddWithValue("@name", spell.Name);
            command.Parameters.AddWithValue("@source", spell.Source);
            command.Parameters.AddWithValue("@level", spell.Level);
            command.Parameters.AddWithValue("@school", spell.School);
            command.Parameters.AddWithValue("@castingtime", spell.CastingTime);
            command.Parameters.AddWithValue("@range", spell.Range);
            command.Parameters.AddWithValue("@components", spell.Components);
            command.Parameters.AddWithValue("@duration", spell.Duration);
            command.Parameters.AddWithValue("@description", spell.Description);
            command.Parameters.AddWithValue("@upcast", spell.Upcast);
            command.ExecuteNonQuery();
        }

        public void UpdateSpell(
            SQLiteConnection connection,
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
            string updateQuery = @"
                UPDATE Spells
                SET 
                    Source = @source,
                    Level = @level,
                    School = @school,
                    CastingTime = @castingtime,
                    Range = @range,
                    Components = @components,
                    Duration = @duration,
                    Description = @description,
                    Upcast = @upcast
                WHERE Name = @name;";


            using var command = new SQLiteCommand(updateQuery, _connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@source", source);
            command.Parameters.AddWithValue("@level", level);
            command.Parameters.AddWithValue("@school", school);
            command.Parameters.AddWithValue("@castingtime", castingtime);
            command.Parameters.AddWithValue("@range", range);
            command.Parameters.AddWithValue("@components", components);
            command.Parameters.AddWithValue("@duration", duration);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@upcast", upcast);
            command.ExecuteNonQuery();
        }

        // TODO: Convert into spell retrieval
        public List<Character> RetrieveCharacters()
        {
            string selectQuery = "SELECT * FROM Characters";
            using var command = new SQLiteCommand(selectQuery, _connection);
            using var reader = command.ExecuteReader();

            var characters = new List<Character>();
            while (reader.Read())
            {
                var character = new Character(
                    reader["Name"].ToString()!,
                    reader["Race"].ToString()!,
                    reader["Class"].ToString()!,
                    Convert.ToInt32(reader["Level"]),
                    Convert.ToInt32(reader["Strength"]),
                    Convert.ToInt32(reader["Dexterity"]),
                    Convert.ToInt32(reader["Constitution"]),
                    Convert.ToInt32(reader["Intelligence"]),
                    Convert.ToInt32(reader["Wisdom"]),
                    Convert.ToInt32(reader["Charisma"])
                );
                characters.Add(character);
            }
            return characters;
        }
    }
}
