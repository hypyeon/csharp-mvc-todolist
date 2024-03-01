using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult New()
    // changing `CreateForm()` to `New()` (RESTful convention)
    {
      return View();
    }

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

    [HttpGet("/items/{id}")]
    public ActionResult Show(int id)
    {
      Item foundItem = Item.Find(id);
      return View(foundItem);
    }

    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()
    {
      Item.ClearAll();
      return View();
    }
  }
}