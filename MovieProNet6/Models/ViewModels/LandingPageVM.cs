using MovieProNet6.Models.Database;
using MovieProNet6.Models.TMDB;

namespace MovieProNet6.Models.ViewModels
{
    public class LandingPageVM
    {
        public List<Collection> CustomCollections { get; set; }
        public MovieSearch NowPlaying { get; set; }
        public MovieSearch Popular { get; set; }
        public MovieSearch TopRated { get; set; }
        public MovieSearch Upcoming { get; set; }       

       
    }
}
