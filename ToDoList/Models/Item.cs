using System.Collections.Generic;
using MySqlConnector;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    // navigation property in Item model that creates one-to-many relationship between Category and Item
    public List<ItemTag> JoinEntities { get; }
  }
}
