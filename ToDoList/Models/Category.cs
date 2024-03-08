using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; }
    // Category including a reference to a related entity like Item makes it a "navigation property"
    // Item is a "collection navigation property" for it containing multiple entities, collection List<> of multiple Item objects. 
  }
}