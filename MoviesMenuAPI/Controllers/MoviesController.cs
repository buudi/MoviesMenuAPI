using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesMenuAPI.Contexts;
using MoviesMenuAPI.Models;
using MoviesMenuAPI.Services;

namespace MoviesMenuAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MyBootcampDbContext _dbContext = new();
    private readonly MovieService _movieService = new();

    // GET: api/movies
    [HttpGet]
    public IEnumerable<Movie> Get()
    {
        return _dbContext.Movies;
    }

    // GET api/movies/1
    [HttpGet("{id}")]
    public ActionResult<Movie> Get(int id)
    {
        var movieItem = _dbContext.Movies.Find(id);
        if (movieItem == null)
            return NotFound();
        return movieItem;
    }

    // POST api/movies
    [HttpPost]
    public IActionResult Post(Movie movie)
    {
        if (movie == null)
            return BadRequest("Please Enter Valid Information");

        var result =  _movieService.AddMovie(movie);

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    // PUT api/movies/1
    [HttpPut("{id}")]
    public IActionResult Put(int id, Movie updatedMovie)
    {
        updatedMovie.Id = id;
        _movieService.ModifyMovie(updatedMovie);
        return NoContent();
    }

    // DELETE api/movies/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var movie = _dbContext.Movies.Find(id);
        if (movie == null) return BadRequest(string.Empty);

        _movieService.RemoveMovie(id);

        return Ok($"Movie: {movie.Title} is deleted from the database successfully");
    }
}
