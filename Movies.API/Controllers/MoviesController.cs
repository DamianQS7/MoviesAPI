using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Models;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.API.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost(EndPoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        // Create a movie object from the request contract object
        Movie movie = request.MapToMovie();

        // Add the new movie to the DB
        await _movieService.CreateAsync(movie);

        // Map the Movie object to a MovieResponse object
        MovieResponse response = movie.MapToMovieResponse();

        // return the URL for the location of the new resource, and the new resource.
        return CreatedAtAction(nameof(GetMovieById), new { idOrSlug = response.Id }, response);
    }

    [HttpGet(EndPoints.Movies.Get)]
    public async Task<IActionResult> GetMovieById([FromRoute] string idOrSlug)
    {
        Movie? movie = Guid.TryParse(idOrSlug, out Guid id)
                    ? await _movieService.GetByIdAsync(id)
                    : await _movieService.GetBySlugAsync(idOrSlug);

        if (movie is null)
            return NotFound();

        MovieResponse response = movie.MapToMovieResponse();
        return Ok(response);
    }

    [HttpGet(EndPoints.Movies.GetAll)]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllAsync();

        var response = movies.MapToMoviesResponse();

        return Ok(response);
    }

    [HttpPut(EndPoints.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateMovieRequest request)
    {
        var movie = request.MapToMovie(id);
        var updatedMovie = await _movieService.UpdateAsync(movie);

        if (updatedMovie is null)
            return NotFound();

        var response = updatedMovie.MapToMovieResponse();
        return Ok(response);
    }

    [HttpDelete(EndPoints.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool isDeleted = await _movieService.DeleteByIdAsync(id);

        if (!isDeleted)
            return NotFound();

        return Ok();
    }
}
