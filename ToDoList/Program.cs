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


/*
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();


using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ToDoList
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}
*/