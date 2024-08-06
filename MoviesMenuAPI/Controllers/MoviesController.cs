﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Get()
    {
        var moviesToReturn = await _dbContext.Movies.ToListAsync();
        return Ok(moviesToReturn);
    }
    //public IEnumerable<Movie> Get()
    //{
    //    return _dbContext.Movies;   
    //}

    // GET api/movies/1
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movieItem = await _dbContext.Movies.FindAsync(id);
        if (movieItem == null)
            return NotFound();
        return Ok(movieItem);
    }
    //public ActionResult<Movie> Get(int id)
    //{
    //    Movie movieItem = _dbContext.Movies.Find(id);
    //    if (movieItem == null) 
    //        return NotFound();
    //    return movieItem;
    //}

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
    //public IActionResult Post(Movie movie)
    //{
    //    if (movie == null)
    //        return BadRequest("Please Enter Valid Information");

    //    var result =  _movieService.AddMovie(movie);

    //    return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    //}

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
    //public IActionResult Put(int id, Movie updatedMovie)
    //{
    //    updatedMovie.Id = id;
    //    _movieService.ModifyMovie(updatedMovie);
    //    return NoContent();
    //}

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

    //public IActionResult Delete(int id)
    //{
    //    var movie = _dbContext.Movies.Find(id);
    //    if (movie == null) return BadRequest(string.Empty);

    //    _movieService.RemoveMovie(id);

    //    return Ok($"Movie: {movie.Title} is deleted from the database successfully");
    //}
}
