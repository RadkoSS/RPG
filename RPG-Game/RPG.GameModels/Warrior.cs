namespace RPG.GameModels;

using static Common.GameConstants.WarriorConstants;

public class Warrior : BaseGameModel
{
    public Warrior()
    {
        this.Strength = DefaultStrength;
        this.Agility = DefaultAgility;
        this.Intelligence = DefaultIntelligence;
        this.Range = DefaultRange;
        this.Symbol = DefaultSymbol;

        base.Setup();
    }
}