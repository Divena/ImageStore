using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ImageStore.Models;

namespace ImageStore.Infrastructure
{
    public class ImageContext : DbContext
    {
        public ImageContext() : base("ISDb")
        { }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<AllActions> UsersActions { get; set; }

    }
}