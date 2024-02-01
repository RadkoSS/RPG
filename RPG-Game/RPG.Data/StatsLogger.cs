namespace RPG.Data;

using Models;
using Contracts;
using GameModels;

public class StatsLogger : IStatsLogger
{
    private readonly RpgDbContext dbContext;

    public StatsLogger(RpgDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task LogCharacterChoice(BaseGameModel character, int raceId)
    {
        RaceStat stats = new RaceStat
        {
            RaceId = raceId,
            Agility = character.Agility,
            Damage = character.Damage,
            Health = character.Health,
            Mana = character.Mana,
            Intelligence = character.Intelligence,
            Range = character.Range,
            Strength = character.Strength
        };

        GameLog gameLog = new GameLog
        {
            Stats = stats
        };

        await this.dbContext.GameLogs.AddAsync(gameLog);

        await this.dbContext.SaveChangesAsync();
    }
}