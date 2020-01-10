using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.OrderedItems
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public OrderedItem OrderedItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderedItem = await _context.OrderedItems
                .Include(o => o.FoodItem)
                .Include(o => o.Person).FirstOrDefaultAsync(m => m.OrderedItemId == id);

            if (OrderedItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
