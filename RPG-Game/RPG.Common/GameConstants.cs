namespace RPG.Common;

public static class GameConstants
{
    public static class CharacterSelectConstants
    {
        public const int WarriorMenuNumber = 1;
        public const int ArcherMenuNumber = 2;
        public const int MageMenuNumber = 3;

        public const int MaxBuffPoints = 3;
    }

    public static class MatrixFieldConstants
    {
        public const int FieldSize = 10;
        public const char DefaultFieldSymbol = '|';
    }

    public static class Multipliers
    {
        public const int StrengthMultiplierForCalculatingHealth = 5;
        public const int IntelligenceMultiplierForCalculatingMana = 3;
        public const int AgilityMultiplierForCalculatingDamage = 2;
    }

    public static class MageConstants
    {
        public const int DefaultStrength = 2;
        public const int DefaultAgility = 1;
        public const int DefaultIntelligence = 3;
        public const int DefaultRange = 3;
        public const char DefaultSymbol = '*';
    }

    public static class WarriorConstants
    {
        public const int DefaultStrength = 3;
        public const int DefaultAgility = 3;
        public const int DefaultIntelligence = 0;
        public const int DefaultRange = 1;
        public const char DefaultSymbol = '@';
    }

    public static class ArcherConstants
    {
        public const int DefaultStrength = 2;
        public const int DefaultAgility = 4;
        public const int DefaultIntelligence = 0;
        public const int DefaultRange = 2;
        public const char DefaultSymbol = '#';
    }

    public static class MonsterConstants
    {
        public const int MinStrength = 1;
        public const int MaxStrength = 4;
        public const int MinAgility = 1;
        public const int MaxAgility = 4;
        public const int MinIntelligence = 1;
        public const int MaxIntelligence = 4;

        public const int DefaultRange = 1;
        public const char DefaultMonsterSymbol = 'O';
    }
}