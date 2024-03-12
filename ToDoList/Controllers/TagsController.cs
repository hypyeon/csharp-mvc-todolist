using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class TagsController : Controller
  {
    private readonly ToDoListContext _db;

    public TagsController(ToDoListContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Tags.ToList());
    }

    public ActionResult Details(int id)
    {
      Tag tag = _db.Tags
        .Include(t => t.JoinEntities)
        // loading `JoinEntities` property of each `Tag`
        // not actual item obj related to a `Tag` 
        .ThenInclude(join => join.Item)
        // loading `Item` obj associated with each `ItemTag`
        .FirstOrDefault(t => t.TagId == id);
      return View(tag);
    }
  }
}