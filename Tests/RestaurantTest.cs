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
      Restaurant firstRestaurant = new Restaurant("InNOut", false, 0);
      Restaurant secondRestaurant = new Restaurant("InNOut", false, 0);
      //assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_Works()
    {
      //arrange, act
      Restaurant testRestaurant = new Restaurant("InNOut", false, 0);

      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant> {testRestaurant};
      //assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantinDB()
    {
      //arrange, act
      Restaurant testRestaurant = new Restaurant("InNOut", false, 0);
      testRestaurant.Save();

      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());
      //assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
