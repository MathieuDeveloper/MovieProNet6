namespace MovieProNet6.Models.Database
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation property

        //Children  of Collection
        public ICollection<MovieCollection> MovieCollections { get; set; } = new HashSet<MovieCollection>();
    }
}
