using Microsoft.EntityFrameworkCore;
using MoviesMenuAPI.Contexts;
using MoviesMenuAPI.Models;
using System.Text;

namespace MoviesMenuAPI.Services;

public class MovieService(MyBootcampDbContext dbContext)
{
    private MyBootcampDbContext _dbContext = dbContext;

    public Movie? GetMovieById(int id) => _dbContext.Movies.FirstOrDefault(m => m.Id == id);
    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }

    // adding AsNoTracking() will make EF Core not track this call (we dont need tracking here since listing all movies is read only, i.e. we don't need to modify it) this improves performance.
    public List<Movie> ListAllMovies() => _dbContext.Movies.AsNoTracking().ToList();
    public async Task<List<Movie>> ListAllMoviesAsync()
    {
        return await _dbContext.Movies.AsNoTracking().ToListAsync();
    }

    public string AddMovie(Movie movie)
    {
        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();

        return $"{movie.Title} added to the database successfully!";
    }
    public async Task<string> AddMovieAsync(Movie movie)
    {
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        return $"{movie.Title} added to the database successfully!";
    }

    public string ModifyMovie(Movie updatedMovie)
    {
        var movieEntity = _dbContext.Movies.FirstOrDefault(m => m.Id == updatedMovie.Id);

        if (movieEntity != null)
        {
            movieEntity.Title = updatedMovie.Title;
            movieEntity.Director = updatedMovie.Director;
            movieEntity.ReleaseYear = updatedMovie.ReleaseYear;
            movieEntity.Genre = updatedMovie.Genre;
            movieEntity.Price = updatedMovie.Price;

            _dbContext.SaveChanges();
        }

        return $"{updatedMovie.Title} modified in the database successfully!";
    }

    public async Task<string> ModifyMovieAsync(Movie updatedMovie)
    {
        var movieEntity = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == updatedMovie.Id);

        if (movieEntity != null)
        {
            movieEntity.Title = updatedMovie.Title;
            movieEntity.Director = updatedMovie.Director;
            movieEntity.ReleaseYear = updatedMovie.ReleaseYear;
            movieEntity.Genre = updatedMovie.Genre;
            movieEntity.Price = updatedMovie.Price;

            await _dbContext.SaveChangesAsync();
        }
        return $"{updatedMovie.Title} modified in the database successfully!";
    }

    public string RemoveMovie(Movie movieToDelete)
    {
        _dbContext.Movies.Remove(movieToDelete);
        _dbContext.SaveChanges();

        return $"Movie: {movieToDelete.Title} is removed from the database successfully";
    }

    public bool CheckMovieExists(int? id) => _dbContext.Movies.Any(m => m.Id == id);
}
