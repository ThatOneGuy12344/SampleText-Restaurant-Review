using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Data
{
    public class SampleText_Restaurant_ReviewContext : IdentityDbContext<ApplicationUser>
    {
        public SampleText_Restaurant_ReviewContext (DbContextOptions<SampleText_Restaurant_ReviewContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<SampleText_Restaurant_Review.Models.Restaurant> Restaurant { get; set; }
        public DbSet<SampleText_Restaurant_Review.Models.Review> Reviews { get; set; }
        public DbSet<SampleText_Restaurant_Review.Models.Roles> Roles { get; set; }
    }
}
