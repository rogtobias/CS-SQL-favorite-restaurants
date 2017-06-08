using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace FavoriteRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _=>
      {
        return View["index.cshtml"];
      };

      Get["/restaurants"] = _ =>
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };

      Get["/cuisine"] = _ =>
      {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["cuisine_types.cshtml", allCuisine];
      };

      Get["/cuisine/new"] = _ =>
      {
        return View["cuisine_form.cshtml"];
      };

      Post["/cuisine/new"] = _ =>
      {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-type"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };

      Get["/restaurants/new"] = _ =>
      {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["restaurant_form.cshtml", allCuisine];
      };

      Post["/restaurants/new"] = _ =>
      {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"],Request.Form["restaurant-alchohol"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };
      Post["/restaurants/delete"] = _ => {
        Restaurant.DeleteAll();
        return View["cleared.cshtml"];
      };
      Post["/cuisine/clear"] = _ => {
        Cuisine.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };
      Get["/cuisine/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", cuisineRestaurants);
        return View["cuisine.cshtml", model];
      };
      Patch["cuisine/edit/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Update(Request.Form["cuisine-type"]);
        return View["success.cshtml"];
      };
      Get["cuisine/delete/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", SelectedCuisine];
      };
      Delete["cuisine/delete/{id}"] = parameters =>
      {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
