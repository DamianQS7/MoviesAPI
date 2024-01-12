﻿using Movies.Application.Models;

namespace Movies.Application.Repositories;

public class MovieRepostitory : IMovieRepository
{
    private readonly List<Movie> _movies = new();

    public Task<bool> CreateAsync(Movie movie)
    {
        _movies.Add(movie);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        int removedCount = _movies.RemoveAll(m => m.Id == id);
        bool wasMovieRemoved = removedCount > 0;
        return Task.FromResult(wasMovieRemoved);
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return Task.FromResult(_movies.AsEnumerable());
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        Movie? movie = _movies.SingleOrDefault(m => m.Id == id);
        return Task.FromResult(movie);
    }

    public Task<bool> UpdateAsync(Movie movie)
    {
        var movieIndex = _movies.FindIndex(m => m.Id == movie.Id);

        if(movieIndex == -1)
            return Task.FromResult(false);

        _movies[movieIndex] = movie;
        return Task.FromResult(true);
    }
}
