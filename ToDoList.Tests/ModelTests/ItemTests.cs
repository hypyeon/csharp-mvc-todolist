using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {
    public IConfiguration Configuration { get; set; }
    public void Dispose()
    {
      Item.ClearAll();
    }
    public ItemTests()
    {
      IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

      DBConfiguration.ConnectionString = configuration["ConnectionStrings:TestConnection"];
    }

    /*
    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      Item newItem = new Item("test");
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";

      //Act
      Item newItem = new Item(description);
      string result = newItem.Description;

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string updatedDescription = "Do the dishes";
      newItem.Description = updatedDescription;
      string result = newItem.Description;

      //Assert
      Assert.AreEqual(updatedDescription, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ItemList()
    {
      // Arrange
      List<Item> newList = new List<Item> { };

      // Act
      List<Item> result = Item.GetAll();

      // Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
    {
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      int result = newItem.Id;

      Assert.AreEqual(1, result);
    }
    */

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      newItem1.Save();
      Item newItem2 = new Item(description02);
      newItem2.Save();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      Item newItem = new Item("Walk the dog");
      newItem.Save();
      Item newItem2 = new Item("Wash dishes");
      newItem2.Save();

      Item foundItem = Item.Find(newItem.Id);

      Assert.AreEqual(newItem, foundItem);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      Item firstItem = new Item("Mow the lawn");
      Item secondItem = new Item("Mow the lawn");
      // won't pass without `Equals()` - since comparing their two references, not values. 

      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      Item testItem = new Item("Mow the lawn");

      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      CollectionAssert.AreEqual(testList, result);
    }
  }
}