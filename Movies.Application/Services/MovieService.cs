using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRespository;
    private readonly IValidator<Movie> _movieValidator;

    public MovieService(IMovieRepository movieRespository, IValidator<Movie> movieValidator)
    {
        _movieRespository = movieRespository;
        _movieValidator = movieValidator;
    }

    public Task<bool> CreateAsync(Movie movie)
    {
        return _movieRespository.CreateAsync(movie);
    }

    public Task<bool> DeleteByIdAsync(Guid Id)
    {
        return _movieRespository.DeleteByIdAsync(Id);
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return _movieRespository.GetAllAsync();
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        return _movieRespository.GetByIdAsync(id);
    }

    public Task<Movie?> GetBySlugAsync(string slug)
    {
        return _movieRespository.GetBySlugAsync(slug);
    }

    public async Task<Movie?> UpdateAsync(Movie movie)
    {
        var movieExists = await _movieRespository.ExistsByIdAsync(movie.Id);

        if(!movieExists)
            return null;
        
        await _movieRespository.UpdateAsync(movie);
        return movie;
    }
}
