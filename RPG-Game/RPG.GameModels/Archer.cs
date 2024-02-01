namespace RPG.GameModels;

using static Common.GameConstants.ArcherConstants;

public class Archer : BaseGameModel
{
    public Archer()
    {
        this.Strength = DefaultStrength;
        this.Agility = DefaultAgility;
        this.Intelligence = DefaultIntelligence;
        this.Range = DefaultRange;
        this.Symbol = DefaultSymbol;

        base.Setup();
    }
}