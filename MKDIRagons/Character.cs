namespace MKDIRagons
{
    internal class Character
    {
        public string Name { get; set; }
        public string? Race { get; set; }
        public string? CharacterClass { get; set; }
        public int Level { get; set; } = 1;
        public int Strength { get; set; } = 10;
        public int Dexterity { get; set; } = 10;
        public int Constitution { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public int Wisdom { get; set; } = 10;
        public int Charisma { get; set; } = 10;

        public Character(string name, string race, string characterClass, int level, int str, int dex, int con, int intel, int wis, int cha)
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
    }
}
