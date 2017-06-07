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
      
    }
