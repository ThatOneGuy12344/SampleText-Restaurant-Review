using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;
using System.ComponentModel.DataAnnotations;

namespace SampleText_Restaurant_Review.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public IndexModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Restaurants { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReviewRestaurant { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from r in _context.Reviews
                                            orderby r.Restaurant.Name
                                            select r.Restaurant.Name;
            var reviews = from r in _context.Reviews
                          select r;
            if (!string.IsNullOrEmpty(SearchString))
            {
                reviews = reviews.Where(s => s.Reviewer.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(ReviewRestaurant))
            {
                reviews = reviews.Where(s => s.Restaurant.Name == ReviewRestaurant);
            }
            Restaurants = new SelectList(await genreQuery.Distinct().ToListAsync());
            Review = await reviews.Include(item => item.Restaurant).ToListAsync();
        }
    }
}
