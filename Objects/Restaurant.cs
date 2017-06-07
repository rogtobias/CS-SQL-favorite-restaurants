using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace FavoriteRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private bool _alchohol;
    private int _cuisineId;

    public Restaurant(string Name, bool Alchohol, int CuisineId, int Id=0)
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
    public bool GetAlchohol()
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
    public void SetAlchohol(bool newAlchohol)
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurant_list (name, alchohol, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @Alchohol, @CuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter("@RestaurantName", this.GetName());

      SqlParameter alchoholParameter = new SqlParameter("@Alchohol", this.GetAlchohol());

      SqlParameter cuisineIdParameter = new SqlParameter("@CuisineId", this.GetCuisineId());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(alchoholParameter);
      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
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
        bool alchohol = rdr.GetBoolean(2);
        int cuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, alchohol, cuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
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
