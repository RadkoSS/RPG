namespace RPG.Data;

using Microsoft.EntityFrameworkCore;

using Contracts;

public class StatsLogger : IStatsLogger
{
    private DbContext dbContext;

    public StatsLogger(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void LogCharacterChoice()
    {
        throw new NotImplementedException();
    }
}