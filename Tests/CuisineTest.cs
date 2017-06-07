using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FavoriteRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisineDBEmpty()
    {
      //arrange, act
      int result = Cuisine.GetAll().Count;
      //assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTheSame()
    {
      //Arrange, act
      Cuisine firstCuisine = new Cuisine("Spanish");
      Cuisine secondCuisine = new Cuisine("Spanish");
      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDb()
    {
      //arrange, act
      Cuisine testCuisine = new Cuisine("Spanish");
      testCuisine.Save();

      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine> {testCuisine};
      //Assert
      Assert.Equal(result, testList);
    }

    [Fact]
    public void Test_Find_FindCuisineInDB()
    {
      //arrange, act
      Cuisine testCuisine = new Cuisine("Spanish");
      testCuisine.Save();

      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());
      //assert
      Assert.Equal(foundCuisine, testCuisine);
    }

    [Fact]
    public void Test_GetRestaurants_RetrievesAllRestaurantsInCuisineType()
    {
      //arrange, act
      Cuisine testCuisine = new Cuisine("Spanish");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Mom's", false, testCuisine.GetId());
      firstRestaurant.Save();

      Restaurant secondRestaurant = new Restaurant("Dad's", true, testCuisine.GetId());
      secondRestaurant.Save();

      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();
      //assert
      Assert.Equal(resultRestaurantList, testRestaurantList);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }
}
