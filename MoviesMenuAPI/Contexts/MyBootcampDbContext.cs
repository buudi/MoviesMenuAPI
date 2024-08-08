using Microsoft.EntityFrameworkCore;
using MoviesMenuAPI.Models;

namespace MoviesMenuAPI.Contexts;

public class MyBootcampDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyBootcamp;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    public List<Movie> GetInitialMovies()
    {
        return Movies.ToList();
    }
}
