using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vidly.Models
{
    public class VidlyContext : DbContext
    {
        public VidlyContext() : base("VidlyContext") {
            Database.SetInitializer<VidlyContext>(null);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MembershipType> MembershipTypes {get;set;}


    }
}