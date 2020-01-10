using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class OrderedItem
    {
        // aka Product. A part of a persons order.
        public int OrderedItemId { get; set; }

        public int Quantity { get; set; } // Quantity of foodItems
        
        [MaxLength(1024)]
        [Display(Name = "Special instructions", Prompt = "Enter special instructions here...")]
        public string SpecialInstructions { get; set; } = default!;
        
        public int FoodItemId { get; set; } 
        public FoodItem? FoodItem { get; set; } // The meal/food in said  'product'

        public int PersonId { get; set; }
        public Person? Person { get; set; } // the person who ordered the foodItem
    }
}