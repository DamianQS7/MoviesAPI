using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRespository;
    private readonly IValidator<Movie> _movieValidator;
    private readonly IRatingRepository _ratingRepository;

    public MovieService(IMovieRepository movieRespository, IValidator<Movie> movieValidator, IRatingRepository ratingRepository)
    {
        _movieRespository = movieRespository;
        _movieValidator = movieValidator;
        _ratingRepository = ratingRepository;
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

    public Task<IEnumerable<Movie>> GetAllAsync(Guid? userId = default, CancellationToken token = default)
    {
        return _movieRespository.GetAllAsync(userId, token);
    }

    public Task<Movie?> GetByIdAsync(Guid id, Guid? userId = default, CancellationToken token = default)
    {
        return _movieRespository.GetByIdAsync(id, userId, token);
    }

    public Task<Movie?> GetBySlugAsync(string slug, Guid? userId = default, CancellationToken token = default)
    {
        return _movieRespository.GetBySlugAsync(slug, userId, token);
    }

    public async Task<Movie?> UpdateAsync(Movie movie, Guid? userId = default, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);

        var movieExists = await _movieRespository.ExistsByIdAsync(movie.Id, token);

        if(!movieExists)
            return null;
        
        await _movieRespository.UpdateAsync(movie, token);

        if(!userId.HasValue)
        {
            var rating = await _ratingRepository.GetRatingAsync(movie.Id, token);
            movie.Rating = rating;
            return movie;
        }

        var ratings = await _ratingRepository.GetRatingAsync(movie.Id, userId.Value, token);
        movie.Rating = ratings.Rating;
        movie.UserRating = ratings.UserRating;
        return movie;
    }
}
