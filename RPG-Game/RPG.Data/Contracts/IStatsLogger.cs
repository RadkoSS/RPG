namespace RPG.Data.Contracts;

using GameModels;

public interface IStatsLogger
{
    Task LogCharacterChoice(BaseGameModel character, int raceId);
}