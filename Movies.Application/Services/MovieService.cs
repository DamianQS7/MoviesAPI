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

    public async Task<bool> CreateAsync(Movie movie, CancellationToken token)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);
        return await _movieRespository.CreateAsync(movie, token);
    }

    public Task<bool> DeleteByIdAsync(Guid Id, CancellationToken token = default)
    {
        return _movieRespository.DeleteByIdAsync(Id, token);
    }

    public Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
    {
        return _movieRespository.GetAllAsync(token);
    }

    public Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _movieRespository.GetByIdAsync(id, token);
    }

    public Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        return _movieRespository.GetBySlugAsync(slug, token);
    }

    public async Task<Movie?> UpdateAsync(Movie movie, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);

        var movieExists = await _movieRespository.ExistsByIdAsync(movie.Id, token);

        if(!movieExists)
            return null;
        
        await _movieRespository.UpdateAsync(movie, token);
        return movie;
    }
}
