using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<Person> Persons { get; set; } = default!;

        public DbSet<OrderedItem> OrderedItems { get; set; } = default!;

        public DbSet<FoodItem> FoodItems { get; set; } = default!;

        public DbSet<FoodCategory> FoodCategories { get; set; } = default!;
        
        public AppDbContext(DbContextOptions option) : base(option)
        {
        }
    }
}