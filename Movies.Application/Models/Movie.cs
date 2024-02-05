using System.Text.RegularExpressions;

namespace Movies.Application.Models;

public class Movie
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public string Slug => GenerateSlug();
    public required int YearOfRelease { get; set; }
    public required List<string> Genres { get; init; } = new();
    public float? Rating { get; set; }
    public int? UserRating { get; set; }

    private string GenerateSlug()
    {
        string sluggedTitle = Regex.Replace(Title, "[^\\w\\s-]", string.Empty).ToLower().Replace(" ", "-");
        return $"{sluggedTitle}-{YearOfRelease}";
    }
}
