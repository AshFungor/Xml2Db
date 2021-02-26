using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xml2Db.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Origin> Origins { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<Reference> Arrivals { get; set; }
        public DbSet<Reference> Sells { get; set; }
    }
}
