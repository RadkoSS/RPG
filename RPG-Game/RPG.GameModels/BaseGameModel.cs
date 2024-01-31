namespace RPG.GameModels;

using static Common.GameConstants.Multipliers;

public abstract class BaseGameModel
{
    protected BaseGameModel()
    {
        this.Setup();
    }

    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Intelligence { get; set; }

    public int Range { get; set; }

    public char Symbol { get; set; }

    public int Health { get; set; }

    public int Mana { get; set; }

    public int Damage { get; set; }

    public void Setup()
    {
        this.Health = this.Strength * StrengthMultiplierForCalculatingHealth;
        this.Mana = this.Intelligence * IntelligenceMultiplierForCalculatingMana;
        this.Damage = this.Agility * AgilityMultiplierForCalculatingDamage;
    }
}