using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
// LINQ: Language-Integrated Query 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;
    // declares a private and readonly field of type `ToDoListContext`
    // property will hold database connection as the type
    
    public ItemsController(ToDoListContext db)
    // parameter is passed an argument thru dependency injection when web app host is built - the arg passed into the constructor is the service set up in `Program.cs` (.AddDbContext<ToDoListContext> ...)
    {
      _db = db; 
    }

    public ActionResult Index()
    {
      List<Item> model = _db.Items
        .Include(item => item.Category)
        .ToList();
      // `ToList()` method enabled with `using System.Linq` directive 
      // thru this, `Item`s in `List` form is accessible without using a verbose `GetAll()` method with raw SQL 
      // `db`: instance of `ToDoListContext` class 
      // it'll look for an obj named `Items`
      //ViewBag.PageTitle = "View All Items";
      // creating a property that is accessible in _Layout.cshtml file by setting the value of HTML element <title>, as in: <title>@ViewBag.PageTitle</title>
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      // `SelectList()` thru `Microsoft.AspNetCore.Mvc.Rendering` directive
      // <select> list will be created for all categories from database, each <option> will be Category's Name property, the value will be Category's CategoryId. 
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
        return View(item);
      }
      else {
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