using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BeerAvailability.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeerAvailability.Controllers
{
  public class BeersController : Controller
  {
    private readonly BeerAvailabilityContext _db;

    public BeersController(BeerAvailabilityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Beer> model = _db.Beers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Beer Beer)
    {
      _db.Beers.Add(Beer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBeer = _db.Beers
          .Include(Beer => Beer.JoinEntities)
          .ThenInclude(join => join.Store)
          .FirstOrDefault(Beer => Beer.BeerId == id);
      return View(thisBeer);
    }
    public ActionResult Edit(int id)
    {
      var thisBeer = _db.Beers.FirstOrDefault(Beer => Beer.BeerId == id);
      return View(thisBeer);
    }

    [HttpPost]
    public ActionResult Edit(Beer Beer)
    {
      _db.Entry(Beer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBeer = _db.Beers.FirstOrDefault(Beer => Beer.BeerId == id);
      return View(thisBeer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBeer = _db.Beers.FirstOrDefault(Beer => Beer.BeerId == id);
      _db.Beers.Remove(thisBeer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}