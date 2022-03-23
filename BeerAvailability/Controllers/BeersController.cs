using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerAvailability.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
      var model = _db.Beers.OrderBy(beer => beer.Name);
      return View(model.ToList());
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

    [HttpPost]
    public ActionResult DeleteBeer(int BeerId)
    {
      var thisBeer = _db.Beers.FirstOrDefault(Beer => Beer.BeerId == BeerId);
      _db.Beers.Remove(thisBeer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAll()
    {
      foreach(Beer beer in _db.Beers)
      {
      _db.Beers.Remove(beer);
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}