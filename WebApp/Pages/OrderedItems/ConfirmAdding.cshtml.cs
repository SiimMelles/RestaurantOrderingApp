using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.OrderedItems
{
    public class ConfirmAdding : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public ConfirmAdding(DAL.AppDbContext context)
        {
            _context = context;
        }

        public SelectList FoodItemSelectList { get; set; } = default!;

        public SelectList PersonSelectList { get; set; } = default!;

        public int PersonId { get; set; }

        public int FoodItemId { get; set; }

        public FoodItem FoodItem { get; set; }
        
        public IActionResult OnGet(int? id, int? personId)
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
            if (personId == null)
            {
                return RedirectToPage("../Index");
            }
            PersonId = personId.Value;

            FoodItemId = id.Value;
            FoodItem = _context.FoodItems.FirstOrDefault(x => x.FoodItemId == FoodItemId);

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
            var foodItem = await _context.FoodItems.FirstOrDefaultAsync(x => x.FoodItemId == FoodItemId);
            
            var person = await _context.Persons.FindAsync(OrderedItem.PersonId);
            person.SumOfItems += foodItem.Price;
            _context.Persons.Update(person);
            
            var order = await _context.Orders.FindAsync(person.OrderId);
            order.OrderTotal += foodItem.Price;
            _context.Orders.Update(order);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("../Persons/Details", new{id = OrderedItem.PersonId});
        }
    }
}