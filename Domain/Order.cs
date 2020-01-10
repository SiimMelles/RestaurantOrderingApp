using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        public int OrderId { get; set; } // Order is basically a table full of people

        [Display(Name = "Total price of the order")]
        public decimal OrderTotal { get; set; } // sum of all the orders by all the people

        public ICollection<Person>? Persons { get; set; }
        
    }
}