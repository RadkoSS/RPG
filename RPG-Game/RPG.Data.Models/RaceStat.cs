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

    [Required]
    public int Strength { get; set; }

    [Required]
    public int Agility { get; set; }

    [Required]
    public int Intelligence { get; set; }

    [Required]
    public int Range { get; set; }

    [Required]
    public int Health { get; set; }

    [Required]
    public int Mana { get; set; }

    [Required]
    public int Damage { get; set; }
}