using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerAvailability.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeerAvailability.Controllers
{
    public class HomeController : Controller
    {
      private readonly BeerAvailabilityContext _db;

      public HomeController(BeerAvailabilityContext db)
      {
        _db = db;
      }
      public ActionResult Index(string search)
      {
        List<Beer> results = _db.Beers.Where(beer => beer.Name.Contains(search)).ToList();
        return View(results);
      }

      public IActionResult BreweryApi(string breweryName)
        {
            var brewery = Brewery.GetBreweries(breweryName);
            return View(brewery);
        }
    }
}