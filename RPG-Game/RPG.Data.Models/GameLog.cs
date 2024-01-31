namespace RPG.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GameLog
{
    public GameLog()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    [ForeignKey(nameof(Stats))]
    public Guid StatsId { get; set; }

    public RaceStat Stats { get; set; } = null!;
}