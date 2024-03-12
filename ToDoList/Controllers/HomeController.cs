using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.PageTitle = "To Do List";
      return View();
    }
  }
}