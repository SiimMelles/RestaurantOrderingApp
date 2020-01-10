using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.OrderedItems
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public SelectList FoodItemSelectList { get; set; } = default!;

        public SelectList PersonSelectList { get; set; } = default!;

        public int PersonId { get; set; }
        
        public IActionResult OnGet(int? id)
        {
            FoodItemSelectList = new SelectList(_context.FoodItems, 
            nameof(FoodItem.FoodItemId), 
            nameof(FoodItem.Name));
        
            PersonSelectList = new SelectList(_context.Persons, 
            nameof(Person.PersonId), 
            nameof(Person.Name));
            if (id == null)
            {
                return RedirectToPage("../Index");
            }
            PersonId = id.Value;

            return Page();
        }

        [BindProperty]
        public OrderedItem OrderedItem { get; set; } = default!;

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderedItems.Add(OrderedItem);
            var foodItem = await _context.FoodItems.FirstOrDefaultAsync(x => x.FoodItemId == OrderedItem.FoodItemId);
            var person = await _context.Persons.FindAsync(OrderedItem.PersonId);
            person.SumOfItems += OrderedItem.FoodItem.Price;
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Persons/Details", new{id = OrderedItem.PersonId});
        }
    }
}
