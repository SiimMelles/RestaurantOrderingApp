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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<OrderedItem> OrderedItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OrderedItem = await _context.OrderedItems
                .Include(o => o.FoodItem)
                .Include(o => o.Person).ToListAsync();
        }
    }
}
