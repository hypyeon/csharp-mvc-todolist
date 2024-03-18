using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ToDoList
{
  class Program
  {
    static void Main(string[] args)
    {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<ToDoListContext>(
      // adding EF Core as a service to To Do List app
      // `ToDoListContext` is a representation of MySQL database 
        DbContextOptions => DbContextOptions
        // `=>` syntax creates a lambda expression: a way to write an anonymous function in a condensed fashion 
          .UseMySql(
          // enabled with `AddDbContext<T>`
              builder.Configuration["ConnectionStrings:DefaultConnection"], 
              // `appsettings.json` implicitly loaded when beginning the process of building web app host by running `WebApplication.CreateBuilder(args)`
              ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
              // version of MySQL server - autodetected 
            )
          )
      );

      builder.Services
        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ToDoListContext>()
        .AddDefaultTokenProviders();
      // added when installing identity

      builder.Services.Configure<IdentityOptions>(options =>
      {
        // Default Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
        // values can be changed (true -> false, 6 -> 2, etc.)
        // when updated, a corresponding update to [RegularExpression] validation attribute for the RegisterViewModel.Password property should also be made
      });

      WebApplication app = builder.Build();

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();
      // added when installing identity 

      app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
      );

      app.Run();
    }
  }
}