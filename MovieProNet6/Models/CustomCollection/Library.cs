using MovieProNet6.Models.Database;

namespace MovieProNet6.Models.CustomCollection
{
    public class Library
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int MovieId { get; set; }        

        public Collection Collection { get; set; }
        public Movie Movie { get; set; }
    }
}
