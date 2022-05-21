namespace MovieProNet6.Models.Database
{
    public class MovieCast
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        public int CastID { get; set; }
        public string Departement { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }
        public string ImageUrl { get; set; }

        public Movie Movie { get; set; }

    }
}
