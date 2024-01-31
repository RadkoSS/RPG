namespace RPG.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.DbConstraints;

public class Race
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(RaceNameMaxLength)]
    public string Name { get; set; } = null!;
}