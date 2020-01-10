using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int PersonId { get; set; }

        [Display(Name = "Total sum of items")]
        public decimal SumOfItems { get; set; } = 0;// Sum of the items ordered
        
        [MinLength(2)]
        [MaxLength(128)]
        [Display(Name = "Name of the person", Prompt = "Enter the name of the person here...")]
        public string Name { get; set; } = default!; // Name of the person

        public ICollection<OrderedItem>? OrderedItems { get; set; }
        
        public int OrderId { get; set; }
        public Order? Order { get; set; } // the person who ordered the foodItem
    }
}