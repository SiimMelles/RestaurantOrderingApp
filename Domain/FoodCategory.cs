using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }
        
        [MinLength(2)]
        [MaxLength(128)]
        [Display(Name = "Category name", Prompt = "Enter category name here...")]
        public string CategoryName { get; set; } = default!; // Name of the foodCategory. a la "Main Course", "Dessert"

        public ICollection<FoodItem>? FoodItems { get; set; }
    }
}