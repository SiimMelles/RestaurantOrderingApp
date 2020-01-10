using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        
        [Display(Name = "Price for the dish", Prompt = "Enter price here...")]
        public decimal Price { get; set; } // price for 1 quantity

        [MinLength(2)]
        [MaxLength(128)]
        [Display(Name = "Name of the dish", Prompt = "Enter name here...")]
        public string Name { get; set; } = default!; // name of the meal

        [MaxLength(128)]
        [Display(Name = "Ingredients", Prompt = "Enter ingredients contained in the dish here...")]
        public string Ingredients { get; set; } = default!; // to allow searching for fooditem

        public ICollection<OrderedItem>? OrderedItems { get; set; }
        
        [Display(Name = "Food category")]
        public int FoodCategoryId { get; set; }
        
        [Display(Name = "Dish category")]
        public FoodCategory? FoodCategory { get; set; }
        
    }
}