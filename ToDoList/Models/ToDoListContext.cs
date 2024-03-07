using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext
  // extending from EF Core's `DbContext` class 
  // ensuring inclusion of all default built-in `DbContext` functionality
  {
    public DbSet<Item> Items { get; set; }
    // declaring an entity called Items in ToDoList database context 

    public ToDoListContext(DbContextOptions options) : base(options) { }
    // a constructor inheriting behavior of its parent class 
    // invoking constructor behavior from `DbContext` class
    // `base` = `DbContext` class 
  }
}