using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ToDoList.Models;
using System.Threading.Tasks;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
  public class AccountController : Controller
  {
    private readonly ToDoListContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ToDoListContext db)
    // set by using dependency injection (refer to `Program.cs`)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    // creating user accounts is an asynchronous action 
    // the built-in `Task<TResult>` class represents asynchronous actions that haven't been completed yet
    {
      if (!ModelState.IsValid)
      // using validation attributes in register's model
      {
        return View(model);
      }
      else
      {
        ApplicationUser user = new ApplicationUser { UserName = model.Email };
        // creating a new `ApplicationUser` with the `Email` from the form submission as its `UserName`
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        // invoking an async method
        // _userManager represents ID's UserManager<TUser> class injected as a service into the AccountController 
        // .CreateAsync method creates a user with the provided pw 
        // IdentityResult represents whether or not the result is successful
        if (result.Succeeded)
        // Succeeded is a property that contains a bool 
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
            // IdentityResult obj contains an Errors property, a type of IEnnumerable<IdentityError> 
            // it is an iterable collection of IdentityError objects and each of them has a Description property containing a string 
          }
          return View(model);
        }
      }
    }

    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        // an async method that allows users to sign in with a pw 
        // PasswordSignInAsync() takes 4 parameters with 2 boolean values
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
          // avoiding specific error messages that could improve user ability to break into an account 
          return View(model);
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}