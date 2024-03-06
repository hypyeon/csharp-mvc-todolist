using System.Collections.Generic;
using MySqlConnector;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; set; }
    //private static List<Item> _instances = new List<Item> { };

    public Item(string description)
    {
      Description = description;
    }

    public Item(string description, int id)
    {
      Description = description;
      //_instances.Add(this);
      //Id = _instances.Count;
      // removing the two above after connecting to MySQL
      Id = id;
    }

    public static List<Item> GetAll()
    {
      //return _instances;
      List<Item> allItems = new List<Item> {};

      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();
      // open a database connection

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items";
      // construct a SQL query

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      // return the query results from the database 

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void ClearAll()
    {
      // _instances.Clear();
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM items";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    
    public override bool Equals(System.Object otherItem)
    // `Equals()` is built-in method in C#
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        // implicit conversions between newItem and otherItem
        bool idEquality = (this.Id == newItem.Id);
        bool descriptionEquality = (this.Description == newItem.Description);
        return (idEquality && descriptionEquality);
      }
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = "INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ItemDescription";
      // must match the param in the command string above 
      param.Value = this.Description;
      cmd.Parameters.Add(param);
      // passing param into the commands' Parameters property
      // if more parameters to add, would nee to `Add()` each one
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Item Find(int id)
    {
      // return _instances[searchId - 1];
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items WHERE id = @ThisId;";

      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      // for when it does not return a result 
      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      Item foundItem = new Item(itemDescription, itemId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }
  }
}
