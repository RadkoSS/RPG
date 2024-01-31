namespace RPG.Core;

using Data;
using Contracts;
using Data.Contracts;

internal class Program
{
    static async Task Main()
    {
        RpgDbContext dbContext = new RpgDbContext();

        IStatsLogger logger = new StatsLogger(dbContext);

        IGameEngine engine = new GameEngine(logger);

        await engine.PlayGame();
    }
}