namespace Movies.API;

public static class EndPoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = "api/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
