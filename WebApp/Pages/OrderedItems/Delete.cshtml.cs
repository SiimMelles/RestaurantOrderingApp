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
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderedItem OrderedItem { get; set; } = default!;

        public int PersonId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderedItem = await _context.OrderedItems
                .Include(o => o.FoodItem)
                .Include(o => o.Person).FirstOrDefaultAsync(m => m.OrderedItemId == id);
            
            PersonId = OrderedItem.PersonId;
            if (OrderedItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            OrderedItem = await _context.OrderedItems.FindAsync(id);

            PersonId = OrderedItem.PersonId;

            if (OrderedItem != null)
            {
                var foodItem = await _context.FoodItems.FindAsync(OrderedItem.FoodItemId);
                
                var person = await _context.Persons.FindAsync(PersonId);
                person.SumOfItems -= foodItem.Price * OrderedItem.Quantity;
                _context.Persons.Update(person);
                
                var order = await _context.Orders.FindAsync(person.OrderId);
                order.OrderTotal -= foodItem.Price * OrderedItem.Quantity;
                _context.Orders.Update(order);
                
                _context.OrderedItems.Remove(OrderedItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Persons/Details", new {id = PersonId});
        }
    }
}
