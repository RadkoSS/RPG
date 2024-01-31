﻿namespace RPG.GameModels;

using static Common.GameConstants.MageConstants;

public class Mage : BaseGameModel
{
    public Mage()
    : base()
    {
        this.Strength = DefaultStrength;
        this.Agility = DefaultAgility;
        this.Intelligence = DefaultIntelligence;
        this.Range = DefaultRange;
        this.Symbol = DefaultSymbol;
    }
}