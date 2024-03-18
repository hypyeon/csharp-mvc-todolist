using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// added after adding package using dotnet

namespace ToDoList.Models
{
  public class ToDoListContext : IdentityDbContext<ApplicationUser>
  // replacing `DbContext` class 
  // `ApplicationUser`: a type of `IdentityDbContext`, which tells Identity which class in the app will contain the user account info it will be responsible for authenticating
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    // declaring an entity called Items in ToDoList database context 
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ItemTag> ItemTags { get; set; }

    public ToDoListContext(DbContextOptions options) : base(options) { }
    // a constructor inheriting behavior of its parent class 
    // invoking constructor behavior from `DbContext` class
    // `base` = `DbContext` class 
  }
}