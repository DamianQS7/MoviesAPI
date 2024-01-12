namespace Movies.Contracts;

public class MoviesResponse
{
    public required IEnumerable<MovieResponse> Items { get; set; } = Enumerable.Empty<MovieResponse>();
}
