using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FavoriteRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DBEmpty()
    {
      //arrange, act
      int result = Restaurant.GetAll().Count;
      //assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfSameName()
    {
      //arrange, act
      Restaurant firstRestaurant = new Restaurant("InNOut", 0, 0);
      Restaurant secondRestaurant = new Restaurant("InNOut", 0, 0);
      //assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}