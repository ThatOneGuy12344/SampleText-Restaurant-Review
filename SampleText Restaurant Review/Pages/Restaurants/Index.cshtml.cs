using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public IndexModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<Restaurant> Restaurant { get;set; }

        public async Task OnGetAsync()
        {
            var restaurants = from r in _context.Restaurant
                          select r;
            if (!string.IsNullOrEmpty(SearchString))
            {
                restaurants = restaurants.Where(s => s.Name.Contains(SearchString));
            }
            Restaurant = await restaurants.ToListAsync();
        }
    }
}
