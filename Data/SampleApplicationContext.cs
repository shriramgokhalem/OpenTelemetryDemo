using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Models;

namespace SampleApplication.Data
{
    public class SampleApplicationContext : DbContext
    {
        public SampleApplicationContext (DbContextOptions<SampleApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<SampleApplication.Models.Movie> Movie { get; set; } = default!;
    }
}
