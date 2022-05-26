using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieProNet6.Models.Database;
using MovieProNet6.Models.CustomCollection;

namespace MovieProNet6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Collection> Collection { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieCollection> MovieCollection { get; set; }
        public DbSet<MovieProNet6.Models.CustomCollection.Library>? Library { get; set; }
    }
}