namespace Movies.API;

public static class EndPoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = "api/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
    }
}
