namespace RPG.GameModels;

using static Common.GameConstants.MonsterConstants;

public class Monster : BaseGameModel
{
    public Monster()
    : base()
    {
        this.Strength = new Random().Next(MinStrength, MaxStrength);
        this.Agility = new Random().Next(MinAgility, MaxAgility);
        this.Intelligence = new Random().Next(MinIntelligence, MaxIntelligence);
        this.Range = DefaultRange;
        this.Symbol = DefaultSymbol;
    }
}