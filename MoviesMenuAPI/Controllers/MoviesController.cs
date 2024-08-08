using Microsoft.AspNetCore.Mvc;
using MoviesMenuAPI.Models;
using MoviesMenuAPI.Services;

namespace MoviesMenuAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    // GET: api/movies
    [HttpGet]
    public IEnumerable<Movie> Get() => _movieService.ListAllMovies();

    // GET api/movies/1
    [HttpGet("{id}")]
    public ActionResult<Movie> Get(int id)
    {
        var movieItem = _movieService.GetMovieById(id);
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
        var movieToDelete = _movieService.GetMovieById(id);

        if (movieToDelete == null)
            return NotFound($"Movie with {id} not found");

        var result = _movieService.RemoveMovie(movieToDelete);

        return Ok(result);
    }
}
