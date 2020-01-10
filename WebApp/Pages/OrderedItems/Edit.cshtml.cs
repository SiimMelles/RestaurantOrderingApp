using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.OrderedItems
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["FoodItemId"] = new SelectList(_context.FoodItems, "FoodItemId", "Ingredients");
           ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "Name");
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

            _context.Attach(OrderedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderedItemExists(OrderedItem.OrderedItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderedItemExists(int id)
        {
            return _context.OrderedItems.Any(e => e.OrderedItemId == id);
        }
    }
}
