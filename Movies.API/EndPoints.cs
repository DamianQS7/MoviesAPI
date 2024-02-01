namespace Movies.API;

public static class EndPoints
{
    public static class Movies
    {
        private const string Base = "api/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Rate = $"{Base}/{{id:guid}}/ratings";
        public const string DeleteRating = $"{Base}/{{id:guid}}/ratings";
    }

    public static class Ratings
    {
        public const string Base = "api/ratings";
        public const string GetUserRatingd = $"{Base}/me";
    }
}
