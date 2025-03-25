using System.Data.SQLite;

namespace MKDIRagons
{
    // IDisposable interface used for freeing up unused resources
    internal class SpellDbHandler : IDisposable
    {
        // _connection is the SQLite DB connection
        private readonly SQLiteConnection _connection;

        // Tracks if an object has been disposed of already
        private bool _disposed = false;

        public SpellDbHandler(string connectionString)
        {
            try
            {
                // Attempt to open SQLite DB connection using _connection
                _connection = new SQLiteConnection(connectionString);
                _connection.Open();
                CreateSpellTable();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                throw;
            }
        }

        private void CreateSpellTable()
        {
            // Table with all required spell features (+ upcast as it is common enough)
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Spells (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL DEFAULT 'N/A' UNIQUE,
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

            try
            {
                // Executes table creation. New table will be created in _connection DB
                using var command = new SQLiteCommand(createTableQuery, _connection);
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
                throw;
            }
        }

        public void InsertSpell(Spell spell)
        {
            string insertQuery = @"
                INSERT OR REPLACE INTO Spells
                    (Name, Source, Level, School, CastingTime, Range, Components, Duration, Description, Upcast)
                VALUES 
                    (@name, @source, @level, @school, @castingtime, @range, @components, @duration, @description, @upcast);";

            try
            {
                // Use Spell.cs class fields as the SQLite table fields
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
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error inserting spell: {ex.Message}");
                throw;
            }
        }

        public void UpdateSpell(Spell spell)
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

            try
            {
                // Use Spell.cs class fields as the SQLite table fields
                using var command = new SQLiteCommand(updateQuery, _connection);
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
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error updating spell: {ex.Message}");
                throw;
            }
        }

        public List<Spell> RetrieveSpells(string? searchTerm = null)
        {
            string selectQuery = "SELECT * FROM Spells";
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Better searching for spells. Now supports searching by spell school or level
                selectQuery += " WHERE Name LIKE @searchTerm OR School LIKE @searchTerm OR Level LIKE @searchTerm";
            }

            try
            {
                using var command = new SQLiteCommand(selectQuery, _connection);
                if (!string.IsNullOrWhiteSpace(searchTerm)) // Checks if searchTerm is not null or whitespace
                {
                    // @searchTerm is a placeholder SQL query.
                    // $"%{searchTerm}%" is a LIKE query, so it searches for similar fields
                    // % is a wildcard to match nay number of characters
                    // For example, %fire% can match to bonfire, fireball, etc
                    command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                }

                using var reader = command.ExecuteReader();
                var spells = new List<Spell>();

                // While reader is reading from the table:
                while (reader.Read())
                {
                    var spell = new Spell(
                        reader["Name"].ToString()!,
                        reader["Source"].ToString()!,
                        reader["Level"].ToString()!,
                        reader["School"].ToString()!,
                        reader["CastingTime"].ToString()!,
                        reader["Range"].ToString()!,
                        reader["Components"].ToString()!,
                        reader["Duration"].ToString()!,
                        reader["Description"].ToString()!,
                        reader["Upcast"].ToString()!
                    );
                    spells.Add(spell);
                }
                return spells;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error retrieving spells: {ex.Message}");
                throw;
            }
        }

        public void DeleteSpell(Spell spell)
        {
            string deleteQuery = "DELETE FROM Spells WHERE Name = @name;";

            try
            {
                using var command = new SQLiteCommand(deleteQuery, _connection);
                command.Parameters.AddWithValue("@name", spell.Name);
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error deleting spell: {ex.Message}");
                throw;
            }
        }

        // Implemented IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Close();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}