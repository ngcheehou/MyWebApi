using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using System.Collections.Generic;

namespace MyWebApi.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SKU> SKUs { get; set; }
    }
}
