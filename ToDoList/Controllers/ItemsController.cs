using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    /*
    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }
    */

    [HttpGet("/categories/{categoryId}/items/new")]
    public ActionResult New(int categoryId)
    // changing `CreateForm()` to `New()` (RESTful convention)
    {
      Category category = Category.Find(categoryId);
      return View(category);
    }

    /*
    [HttpPost("/items")]
    public ActionResult Create(string description)
    // this route is invoked when form is submitted
    // str param (description) matches the `name` attribute 
    {
      Item myItem = new Item(description);
      //return View("Index", myItem);
      // instead of `View()`, because no longer routing to a view with the same exact name as our route method
      return RedirectToAction("Index");
    }
    */

    [HttpGet("/categories/{categoryId}/items/{itemId}")]
    public ActionResult Show(int categoryId, int itemId)
    {
      Item item = Item.Find(itemId);
      Category category = Category.Find(categoryId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("item", item);
      model.Add("category", category);
      return View(model);
    }

    /*
    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()
    {
      Item.ClearAll();
      return View();
    }
    */
  }
}