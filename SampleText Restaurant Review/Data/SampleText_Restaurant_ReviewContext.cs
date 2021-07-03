using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Data
{
    public class SampleText_Restaurant_ReviewContext : DbContext
    {
        public SampleText_Restaurant_ReviewContext (DbContextOptions<SampleText_Restaurant_ReviewContext> options)
            : base(options)
        {
        }

        public DbSet<SampleText_Restaurant_Review.Models.Restaurant> Restaurant { get; set; }
        public DbSet<SampleText_Restaurant_Review.Models.Review> Reviews { get; set; }
    }
}
