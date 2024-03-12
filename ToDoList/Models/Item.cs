using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class Item
  {
    [Required(ErrorMessage = "The item's description can't be empty.")]
    public string Description { get; set; }
    
    public int ItemId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "You must add your item to a category. Have you created a category yet?")]
    public int CategoryId { get; set; }
    // validation attribute (for `CategoryId`): value must be a number between 1 to the `int.MaxValue` (max possible integer value in C#)

    public Category Category { get; set; }
    // navigation property in Item model that creates one-to-many relationship between Category and Item
    public List<ItemTag> JoinEntities { get; }
  }
}
