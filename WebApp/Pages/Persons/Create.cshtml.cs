using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Person Person { get; set; } = default!;

        public int OrderId { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./../Index");
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            OrderId = id.Value;
            return Page();
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Persons.Add(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("./../Orders/Details", new {id = Person.OrderId});
        }
    }
}
