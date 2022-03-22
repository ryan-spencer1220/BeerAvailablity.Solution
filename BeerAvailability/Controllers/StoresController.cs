using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BeerAvailability.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BeerAvailability.Controllers
{
  [Authorize]
    public class StoresController : Controller
  {
    private readonly BeerAvailabilityContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public StoresController(UserManager<ApplicationUser> userManager, BeerAvailabilityContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userStores = _db.Stores.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userStores);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Store store, int BeerId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      store.User = currentUser;
      _db.Stores.Add(store);
      _db.SaveChanges();
      if (BeerId != 0)
      {
        _db.Inventory.Add(new Inventory() { BeerId = BeerId, StoreId = store.StoreId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisStore = _db.Stores
          .Include(store => store.JoinEntities)
          .ThenInclude(join => join.Beer)
          .FirstOrDefault(store => store.StoreId == id);
      return View(thisStore);
    }

    public ActionResult Edit(int id)
    {
      var thisStore = _db.Stores.FirstOrDefault(store => store.StoreId == id);
      ViewBag.BeerId = new SelectList(_db.Beers, "BeerId", "Name");
      return View(thisStore);
    }

    [HttpPost]
    public ActionResult Edit(Store store, int BeerId)
    {
      if (BeerId != 0)
      {
        _db.Inventory.Add(new Inventory() { BeerId = BeerId, StoreId = store.StoreId });
      }
      _db.Entry(store).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBeer(int id)
    {
      var thisStore = _db.Stores.FirstOrDefault(store => store.StoreId == id);
      ViewBag.BeerId = new SelectList(_db.Beers, "BeerId", "Name");
      return View(thisStore);
    }

    [HttpPost]
    public ActionResult AddBeer(Store store, int BeerId)
    {
      if (BeerId != 0)
      {
        _db.Inventory.Add(new Inventory() { BeerId = BeerId, StoreId = store.StoreId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      var thisStore = _db.Stores.FirstOrDefault(stores => stores.StoreId == id);
      return View(thisStore);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisStore = _db.Stores.FirstOrDefault(stores => stores.StoreId == id);
      _db.Stores.Remove(thisStore);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBeer(int joinId)
    {
        var joinEntry = _db.Inventory.FirstOrDefault(entry => entry.InventoryId == joinId);
        _db.Inventory.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}