using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
// LINQ: Language-Integrated Query 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
// below added for authorization 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
// to use `UserManager`
using System.Threading.Tasks;
// to call async methods 
using System.Security.Claims;
// to use claim based authorization; to identify a user thru a claim to get only the items associated with that user

namespace ToDoList.Controllers
{
  [Authorize]
  // this allows access to the controller only if a user is logged in 
  // to allow unauthorized users to have access, use [AllowAnonymous]
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;
    // declares a private and readonly field of type `ToDoListContext`
    // property will hold database connection as the type
    private readonly UserManager<ApplicationUser> _userManager;
    
    public ItemsController(UserManager<ApplicationUser> userManager, ToDoListContext db)
    // parameter is passed an argument thru dependency injection when web app host is built - the arg passed into the constructor is the service set up in `Program.cs` (.AddDbContext<ToDoListContext> ...)
    {
      _db = db; 
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // `User`: a property of ItemsController, contains info about currently signed-in user 
      // `FindFirst()`: a method that locates the first user record that meets the provided criteria 
      // `ClaimTypes.NameIdentifier`: an argument to locate the unique ID associated with the currently signed in user account 
      // `?`: an existential operator - by using this, calling the property, only if the method does not return `null`  
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      // `FindByIdAsync`: a built-in id method to find a user's accoutn by their unique ID 
      List<Item> userItems = _db.Items
        .Where(entry => entry.User.Id == currentUser.Id)
        // `Where`: a LINQ method to filter a collection in a way that echoes the logic of SQL Where clause 
        .Include(item => item.Category)
        .ToList();
      return View(userItems);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      // `SelectList()` thru `Microsoft.AspNetCore.Mvc.Rendering` directive
      // <select> list will be created for all categories from database, each <option> will be Category's Name property, the value will be Category's CategoryId. 
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Item item, int CategoryId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
        return View(item);
      }
      else {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        item.User = currentUser; 
        _db.Items.Add(item);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
        .Include(item => item.Category)
        .Include(item => item.JoinEntities)
        // using `.Include()`: can be done for as many navigation properties as there are that need to be fetched 
        .ThenInclude(join => join.Tag)
        .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Items.Update(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    // not "Delete" because both `GET` and `POST` action methods for Delete take `id` as a param 
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult AddTag(Item item, int tagId)
    {
      #nullable enable
      ItemTag? joinEntity = _db.ItemTags.FirstOrDefault(join => (join.TagId == tagId && join.ItemId == item.ItemId));
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.ItemTags.Add(new ItemTag() { TagId = tagId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = item.ItemId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      ItemTag joinEntry = _db.ItemTags
        .FirstOrDefault(entry => entry.ItemTagId == joinId);
      _db.ItemTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}