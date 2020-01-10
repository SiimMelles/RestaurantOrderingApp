using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }
        
        
        public IList<Person> Persons { get; set; } = default!;

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Order = await _context.Orders.Include(m => m.Persons)
                                         .FirstOrDefaultAsync(m => m.OrderId == id);
            
            //Necessary?
            var personsQuery = _context.Persons.Where(c => c.OrderId == id);
            Persons = await personsQuery.ToListAsync();
            
            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
