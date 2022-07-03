//using MovieProNet6.Data;
//using MovieProNet6.Models.Settings;
//using MovieProNet6.Models.TMDB;
//using MovieProNet6.Models;
//using MovieProNet6.Services.Interfaces;

//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Options;
//using MovieProNet6.Enums;
//using MovieProNet6.Models.Settings;
//using MovieProNet6.Models.TMDB;
//using MovieProNet6.Services.Interfaces;
////Tout Mathieu ce fichier
//using System.Runtime.Serialization.Json;

//namespace MovieProNet6.Services
//{
//    public class MovieSearchService
//    {
//        private readonly AppSettings _appSettings;
//        private readonly ApplicationDbContext _context;
//        private readonly IImageService _imageService;
//        private readonly IRemoteMovieService _tmdbMovieService;
//        private readonly IDataMappingService _tmdbMappingService;

//        private readonly IHttpClientFactory _httpClient;

//        public MovieSearchService(IDataMappingService tmdbMappingService, IRemoteMovieService tmdbMovieService, IImageService imageService, ApplicationDbContext context, AppSettings appSettings, IHttpClientFactory httpClient)
//        {
//            _tmdbMappingService = tmdbMappingService;
//            _tmdbMovieService = tmdbMovieService;
//            _imageService = imageService;
//            _context = context;
//            _appSettings = appSettings;

//            _httpClient = httpClient;
            
//        }

//        public async Task<MovieDetail> MovieSearch(string title)
//        {
//            //Step 1: Setup default return object
//            MovieDetail movieDetail = new();

//            //Step 2: Assemble the request
//            var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{title}";
//            var queryParams = new Dictionary<string, string>()
//            {
//                { "api_key", _appSettings.MovieProSettings.TmDbApiKey },
//                { "language", _appSettings.TMDBSettings.QueryOptions.Language},
//                { "append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse}
//            };
//            var requestUri = QueryHelpers.AddQueryString(query, queryParams);

//            //Step 3: Create client and execute request
//            var client = _httpClient.CreateClient();
//            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
//            var response = await client.SendAsync(request);

//            //Step 4: Deserialize into Moviedetail 
//            if (response.IsSuccessStatusCode)
//            {
//                using var responseStream = await response.Content.ReadAsStreamAsync();
//                var dcjs = new DataContractJsonSerializer(typeof(MovieDetail));
//                movieDetail = dcjs.ReadObject(responseStream) as MovieDetail;
//            }
//            return movieDetail;
//        }

//    }
//}
