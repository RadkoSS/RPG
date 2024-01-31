namespace RPG.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RaceStat
{
    public RaceStat()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(Race))]
    public int RaceId { get; set; }

    public Race Race { get; set; } = null!;

    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Intelligence { get; set; }
}