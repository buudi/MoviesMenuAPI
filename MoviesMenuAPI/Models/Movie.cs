using System.ComponentModel.DataAnnotations;

namespace MoviesMenuAPI.Models;

public class Movie
{
    [Key]
    public int? Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Director { get; set; }

    [Required]
    public int? ReleaseYear { get; set; }

    [Required]
    public string? Genre { get; set; }

    [Required]
    public decimal? Price { get; set; }
}
