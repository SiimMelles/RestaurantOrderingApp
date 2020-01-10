using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.OrderedItems
{
    public class CreateFromList : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateFromList(DAL.AppDbContext context)
        {
            _context = context;
        }
        
        public string Search { get; set; }

        public int PersonId { get; set; }
        
        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync(int? id, string search, string toDoActionReset)
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

            if (id != null)
            {
                PersonId = id.Value;
            }
            else
            {
                PersonId = PersonId;
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