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

        public string[] SearchConditions { get; set; } = {""};
        
        public int PersonId { get; set; }
        public int OrderId { get; set; }

        
        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync(int? id, int? orderId, string search, string toDoActionReset)
        {
            if (toDoActionReset == "Reset")
            {
                Search = "";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(search))
                {
                    Search = search.ToLower().Trim();
                    SearchConditions = Search.Split(",");
                }
            }
            if (orderId != null)
            {
                OrderId = orderId.Value;
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

            foreach (var searchCondition in SearchConditions)
            {
                if (!string.IsNullOrWhiteSpace(searchCondition))
                {
                    if (searchCondition.StartsWith("!"))
                    {
                        foodQuery = foodQuery.Where(a => !(a.Ingredients.ToLower().Contains(searchCondition.Substring(1)) && 
                                                         a.Name.ToLower().Contains(searchCondition.Substring(1))));
                    }
                    else
                    {
                        foodQuery = foodQuery.Where(a => a.Ingredients.ToLower().Contains(searchCondition) || 
                                                         a.Name.ToLower().Contains(searchCondition));    
                    }
                }    
            }
            
            
            FoodItem = await foodQuery.ToListAsync();
            
        }

    }
}