﻿using Microsoft.AspNetCore.Mvc;
using MoviesMenuAPI.Models;
using MoviesMenuAPI.Services;

namespace MoviesMenuAPI.Controllers;

[Route("api/movies-async")]
[ApiController]
public class MoviesAsyncController(MovieService movieService) : ControllerBase
{

    private readonly MovieService _movieService = movieService;

    // GET: api/movies-async
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var moviesToReturn = await _movieService.ListAllMoviesAsync();

        return Ok(moviesToReturn);
    }

    // GET api/movies-async/1
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movieItem = await _movieService.GetMovieByIdAsync(id);
        if (movieItem == null)
            return NotFound();
        return Ok(movieItem);
    }

    // POST api/movies-async
    [HttpPost]
    public async Task<IActionResult> Post(Movie movie)
    {
        if (movie == null)
            return BadRequest("Please Enter Valid Information");

        await _movieService.AddMovieAsync(movie);
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    // PUT api/movies-async/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Movie updatedMovie)
    {
        var movieToUpdate = await _movieService.GetMovieByIdAsync(id);
        if (movieToUpdate == null)
            return NotFound($"Invalid Movie Id");

        await _movieService.ModifyMovieAsync(id, updatedMovie);
        return NoContent();
    }

    // DELETE api/movies-async/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movieToDelete = await _movieService.GetMovieByIdAsync(id);
        if (movieToDelete == null) return BadRequest(string.Empty);

        await _movieService.RemoveMovieAsync(movieToDelete);
        return Ok($"Movie: {movieToDelete.Title} is deleted from the database successfully");
    }
}
