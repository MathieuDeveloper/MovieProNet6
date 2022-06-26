﻿using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MovieProNet6.Enums;
using MovieProNet6.Models.Settings;
using MovieProNet6.Models.TMDB;
using MovieProNet6.Services.Interfaces;
using System.Runtime.Serialization.Json;


namespace MovieProNet6.Services
{
    public class TMDBMovieService : IRemoteMovieService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClient;

        public TMDBMovieService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<ActorDetail> ActorDetailAsync(int id)
        {
            //Step 1: Setup a default return object
            ActorDetail actorDetail = new();

            //Step 2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/person/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language}
            };
            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Return the ActorDetail object
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                var dcjs = new DataContractJsonSerializer(typeof(ActorDetail));
                actorDetail = (ActorDetail)dcjs.ReadObject(responseStream);
            }
        
            return actorDetail;
        }

        public async Task<MovieDetail> MovieDetailAsync(int id)
        {
            //Step 1: Setup default return object
            MovieDetail movieDetail = new();

            //Step 2: Assemble the request
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language},
                { "append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse}
            };
            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create client and execute request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Deserialize into Moviedetail 
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var dcjs = new DataContractJsonSerializer(typeof(MovieDetail));
                movieDetail = dcjs.ReadObject(responseStream) as MovieDetail;
            }
            return movieDetail;
        }


        ////Mathieu essaie créer nouvelle class pour importer movie:
        //public async Task<MovieDetail> ImportMovieAsync(string title)
        //{

        //    //Step 1: Setup a default instance of MovieSearch
        //    MovieSearch importMovie = new();

        //    //Step 2: Assemble the full request uri string
        //    var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{title}";

        //    var queryParams = new Dictionary<string, string>()
        //    {
        //        { "api_key", _appSettings.MovieProSettings.TmDbApiKey },
        //        { "language", _appSettings.TMDBSettings.QueryOptions.Language },
        //        { "page", _appSettings.TMDBSettings.QueryOptions.Page }
        //    };

        //    var requestUri = QueryHelpers.AddQueryString(query, queryParams);

        //    //Step 3: Create a client and execute the request
        //    var client = _httpClient.CreateClient();
        //    var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        //    var response = await client.SendAsync(request);

        //    //Step 4: Return the MovieSearch object
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));
        //        using var responseStream = await response.Content.ReadAsStreamAsync();
        //        importMovie = (MovieSearch)dcjs.ReadObject(responseStream);
        //        importMovie.results = importMovie.results.ToArray();
        //        importMovie.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{r.poster_path}");
        //    }

        //    return importMovie;
        //}





        public async Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count)
        {

            //Step 1: Setup a default instance of MovieSearch
            MovieSearch movieSearch = new();

            //Step 2: Assemble the full request uri string
            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

            var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.MovieProSettings.TmDbApiKey },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language },
                { "page", _appSettings.TMDBSettings.QueryOptions.Page }
            };

            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

            //Step 3: Create a client and execute the request
            var client = _httpClient.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await client.SendAsync(request);

            //Step 4: Return the MovieSearch object
            if (response.IsSuccessStatusCode)
            {
                var dcjs = new DataContractJsonSerializer(typeof(MovieSearch));
                using var responseStream = await response.Content.ReadAsStreamAsync();
                movieSearch = (MovieSearch)dcjs.ReadObject(responseStream);
                movieSearch.results = movieSearch.results.Take(count).ToArray();
                movieSearch.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.MovieProSettings.DefaultPosterSize}/{r.poster_path}");
            }

            return movieSearch;
        }





       

    }
}
