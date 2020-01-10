using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.FoodItems
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        
        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public string Search { get; set; }

        
        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync(string search, string toDoActionReset)
        {
            if (toDoActionReset == "Reset")
            {
                Search = null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(search))
                {
                    Search = search.ToLower().Trim();
                }

            }
            
            var foodQuery = _context.FoodItems
                .Include(f => f.FoodCategory).AsQueryable();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                foodQuery = foodQuery.Where(a => a.Ingredients.ToLower().Contains(Search) || 
                                                 a.Name.ToLower().Contains(Search));
            }
            
            FoodItem = await foodQuery.ToListAsync();
            
        }
    }
}
