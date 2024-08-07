using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesMenuAPI.Contexts;
using MoviesMenuAPI.Models;
using MoviesMenuAPI.Services;

namespace MoviesMenuAPI.Controllers;

[Route("api/movies-async")]
[ApiController]
public class MoviesAsyncController : ControllerBase
{
    private readonly MyBootcampDbContext _dbContext = new();
    private readonly MovieService _movieService = new();

    // GET: api/movies
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var moviesToReturn = await _dbContext.Movies.ToListAsync();
        return Ok(moviesToReturn);
    }

    // GET api/movies/1
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movieItem = await _dbContext.Movies.FindAsync(id);
        if (movieItem == null)
            return NotFound();
        return Ok(movieItem);
    }

    // POST api/movies
    [HttpPost]
    public async Task<IActionResult> Post(Movie movie)
    {
        if (movie == null)
            return BadRequest("Please Enter Valid Information");

        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);

    }

    // PUT api/movies/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Movie updatedMovie)
    {
        var movieToUpdate = await _dbContext.Movies.FindAsync(id);
        if (movieToUpdate == null)
            return NotFound($"Invalid Movie Id");

        updatedMovie.Id = id;
        movieToUpdate.Title = updatedMovie.Title;
        movieToUpdate.Director = updatedMovie.Director;
        movieToUpdate.ReleaseYear = updatedMovie.ReleaseYear;
        movieToUpdate.Genre = updatedMovie.Genre;
        movieToUpdate.Price = updatedMovie.Price;

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/movies/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _dbContext.Movies.FindAsync(id);
        if (movie == null) return BadRequest(string.Empty);

        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();

        return Ok($"Movie: {movie.Title} is deleted from the database successfully");
    }
}
