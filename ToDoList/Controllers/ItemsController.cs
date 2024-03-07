using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
// LINQ: Language-Integrated Query 

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
      List<Item> model = _db.Items.ToList();
      // `ToList()` method enabled with `using System.Linq` directive 
      // thru this, `Item`s in `List` form is accessible without using a verbose `GetAll()` method with raw SQL 
      // `db`: instance of `ToDoListContext` class 
      // it'll look for an obj named `Items`
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
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
  }
}