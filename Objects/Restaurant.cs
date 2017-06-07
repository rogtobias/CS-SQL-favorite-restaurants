using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace FavoriteRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _alchohol;
    private int _cuisineId;

    public Restaurant(string Name, int Alchohol, int CuisineId, int Id=0)
    {
      _name = Name;
      _alchohol = Alchohol;
      _cuisineId = CuisineId;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetAlchohol()
    {
      return _alchohol;
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetAlchohol(int newAlchohol)
    {
      _alchohol = newAlchohol;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        return (idEquality && nameEquality);
      }
    }
    
    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant_list;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int alchohol = rdr.GetInt32(2);
        int cuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, alchohol, cuisineId, restaurantId);
      }
       if(rdr != null)
       {
         rdr.Close();
       }
       if(conn != null)
       {
         conn.Close();
       }
       return allRestaurants;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurant_list;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
