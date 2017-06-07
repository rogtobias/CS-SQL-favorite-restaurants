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
  }
}
