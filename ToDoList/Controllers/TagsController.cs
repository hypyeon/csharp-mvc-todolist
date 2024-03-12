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

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Tag tag)
    {
      _db.Tags.Add(tag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddItem(int id)
    {
      Tag tag = _db.Tags.FirstOrDefault(t => t.TagId == id);
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
      return View(tag);
    }
    [HttpPost]
    public ActionResult AddItem(Tag tag, int itemId)
    {
      #nullable enable
      ItemTag? joinEntity = _db.ItemTags
        .FirstOrDefault(join => 
          (join.ItemId == itemId && join.TagId == tag.TagId)
        );
      #nullable disable
      if (joinEntity == null && itemId != 0)
      {
        _db.ItemTags.Add(new ItemTag() { ItemId = itemId, TagId = tag.TagId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = tag.TagId });
    }

    public ActionResult AddTag(int id)
    {
      Item item = _db.Items
        .FirstOrDefault(i => i.ItemId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
      return View(item);
    }
    [HttpPost]
    public ActionResult AddTag(Item item, int tagId)
    {
      #nullable enable
      ItemTag? joinEntity = _db.ItemTags
        .FirstOrDefault(join => 
          (join.TagId == tagId && join.ItemId == item.ItemId)
        );
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.ItemTags.Add(new ItemTag() { TagId = tagId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = item.ItemId });
    }  

    public ActionResult Edit(int id)
    {
      Tag tag = _db.Tags
        .FirstOrDefault(t => t.TagId == id);
      return View(tag);
    }
    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
      _db.Tags.Update(tag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Tag tag = _db.Tags
        .FirstOrDefault(t => t.TagId == id);
      return View(tag);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Tag thisTag = _db.Tags
        .FirstOrDefault(tags => tags.TagId == id);
      _db.Tags.Remove(thisTag);
      _db.SaveChanges();
      return RedirectToAction("Index");
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