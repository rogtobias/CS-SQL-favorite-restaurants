using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace FavoriteRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _type;

    public Cuisine(string CuisineType, int Id=0)
    {
      _type = CuisineType;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetCuisineType()
    {
      return _type;
    }
    public void SetCuisineType(string newType)
    {
      _type = newType;
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine_list;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineType = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(cuisineType, cuisineId);
        allCuisines.Add(newCuisine);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }

    public List<Restaurant> GetRestaurants()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant_list WHERE cuisine_id = @CuisineId", conn);
      SqlParameter cuisineIdParameter = new SqlParameter("@CuisineId", this.GetId());
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Restaurant> restaurants = new List<Restaurant>{};
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        bool alchohol = rdr.GetBoolean(2);
        int cuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, alchohol, cuisineId, restaurantId);
        restaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return restaurants;
    }

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine_list WHERE id=@CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter("@CuisineId", id.ToString());
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineType = null;

      while(rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(0);
        foundCuisineType = rdr.GetString(1);
      }
      Cuisine foundCuisine = new Cuisine(foundCuisineType, foundCuisineId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisine_list (type) OUTPUT INSERTED.id VALUES (@cuisineType);", conn);

      SqlParameter typeParameter = new SqlParameter("@cuisineType", this.GetCuisineType());
      cmd.Parameters.Add(typeParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newType)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisine_list SET type = @NewType OUTPUT INSERTED.type WHERE id = @CuisineId;", conn);

      SqlParameter newTypeParameter = new SqlParameter("@NewType", newType);
      // newTypeParameter.ParameterType = "@newType";
      // newTypeParameter.Value = newType;
      cmd.Parameters.Add(newTypeParameter);

      SqlParameter cuisineIdParameter = new SqlParameter("@CuisineId", this.GetId());
      // cuisineIdParameter.ParameterType = "@CuisineId";
      // cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._type = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine_list;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override int GetHashCode()
    {
      return this.GetCuisineType().GetHashCode();
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
       return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.GetId() == newCuisine.GetId();
        bool cuisineTypeEquality = this.GetType() == newCuisine.GetType();
        return (idEquality && cuisineTypeEquality);
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine_list WHERE id = @CuisineId; DELETE FROM restaurant_list WHERE cuisine_id = @CuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter("@CuisineId", this.GetId());

      cmd.Parameters.Add(cuisineIdParameter);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}
