using System.Data.SQLite;

namespace MKDIRagons
{
    internal class CharacterDbHandler
    {
        private readonly SQLiteConnection _connection;

        public CharacterDbHandler(SQLiteConnection connection)
        {
            _connection = connection;
            CreateCharacter();
        }

        private void CreateCharacter()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Characters (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Race TEXT NOT NULL,
                    Class TEXT NOT NULL,
                    Level INTEGER NOT NULL DEFAULT 1,
                    Strength INTEGER NOT NULL DEFAULT 10,
                    Dexterity INTEGER NOT NULL DEFAULT 10,
                    Constitution INTEGER NOT NULL DEFAULT 10,
                    Intelligence INTEGER NOT NULL DEFAULT 10,
                    Wisdom INTEGER NOT NULL DEFAULT 10,
                    Charisma INTEGER NOT NULL DEFAULT 10
                );";

            using var command = new SQLiteCommand(createTableQuery, _connection);
            command.ExecuteNonQuery();
        }

        public void InsertCharacter(Character character)
        {
            string insertQuery = @"
                INSERT INTO Characters 
                    (Name, Race, Class, Level, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma)
                VALUES 
                    (@name, @race, @class, @level, @strength, @dexterity, @constitution, @intelligence, @wisdom, @charisma);";

            using var command = new SQLiteCommand(insertQuery, _connection);
            command.Parameters.AddWithValue("@name", character.Name);
            command.Parameters.AddWithValue("@race", character.Race);
            command.Parameters.AddWithValue("@class", character.CharacterClass);
            command.Parameters.AddWithValue("@level", character.Level);
            command.Parameters.AddWithValue("@strength", character.Strength);
            command.Parameters.AddWithValue("@dexterity", character.Dexterity);
            command.Parameters.AddWithValue("@constitution", character.Constitution);
            command.Parameters.AddWithValue("@intelligence", character.Intelligence);
            command.Parameters.AddWithValue("@wisdom", character.Wisdom);
            command.Parameters.AddWithValue("@charisma", character.Charisma);
            command.ExecuteNonQuery();
        }
        public void UpdateCharacter(
            SQLiteConnection connection, 
            string name, 
            string race, 
            string characterClass, 
            int level, 
            int strength, 
            int dexterity, 
            int constitution, 
            int intelligence, 
            int wisdom, 
            int charisma)
        {
            string updateQuery = @"
                UPDATE Characters
                SET 
                    Race = @race,
                    Class = @class,
                    Level = @level,
                    Strength = @strength,
                    Dexterity = @dexterity,
                    Constitution = @constitution,
                    Intelligence = @intelligence,
                    Wisdom = @wisdom,
                    Charisma = @charisma
                WHERE Name = @name;";


            using var command = new SQLiteCommand(updateQuery, _connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@race", race);
            command.Parameters.AddWithValue("@class", characterClass);
            command.Parameters.AddWithValue("@level", level);
            command.Parameters.AddWithValue("@strength", strength);
            command.Parameters.AddWithValue("@dexterity", dexterity);
            command.Parameters.AddWithValue("@constitution", constitution);
            command.Parameters.AddWithValue("@intelligence", intelligence);
            command.Parameters.AddWithValue("@wisdom", wisdom);
            command.Parameters.AddWithValue("@charisma", charisma);
            command.ExecuteNonQuery();
        }
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
        public void DeleteCharacter(Character character)
        {
            string deleteQuery = "DELETE FROM Characters WHERE Name = @name;";
            using var command = new SQLiteCommand(deleteQuery, _connection);
            command.Parameters.AddWithValue("@name", character.Name);
            command.ExecuteNonQuery();
        }
    }
}
