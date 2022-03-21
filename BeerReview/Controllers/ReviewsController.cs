using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BeerReview.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BeerReview.Controllers
{
  // [Authorize]
    public class ReviewsController : Controller
  {
    private readonly BeerReviewContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewsController(UserManager<ApplicationUser> userManager, BeerReviewContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userReviews = _db.Reviews.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userReviews);
    }

    public ActionResult Create()
    {
      ViewBag.BeerId = new SelectList(_db.Beers, "BeerId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Review review, int BeerId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      review.User = currentUser;
      _db.Reviews.Add(review);
      _db.SaveChanges();
      if (BeerId != 0)
      {
        _db.BeerRatings.Add(new BeerRating() { BeerId = BeerId, ReviewId = review.ReviewId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisReview = _db.Reviews
          .Include(review => review.JoinEntities)
          .ThenInclude(join => join.Beer)
          .FirstOrDefault(review => review.ReviewId == id);
      return View(thisReview);
    }

    public ActionResult Edit(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(review => review.ReviewId == id);
      ViewBag.BeerId = new SelectList(_db.Beers, "BeerId", "Name");
      return View(thisReview);
    }

    [HttpPost]
    public ActionResult Edit(Review review, int BeerId)
    {
      if (BeerId != 0)
      {
        _db.BeerRatings.Add(new BeerRating() { BeerId = BeerId, ReviewId = review.ReviewId });
      }
      _db.Entry(review).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBeer(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(review => review.ReviewId == id);
      ViewBag.BeerId = new SelectList(_db.Beers, "BeerId", "Name");
      return View(thisReview);
    }

    [HttpPost]
    public ActionResult AddBeer(Review review, int BeerId)
    {
      if (BeerId != 0)
      {
        _db.BeerRatings.Add(new BeerRating() { BeerId = BeerId, ReviewId = review.ReviewId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      return View(thisReview);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisReview = _db.Reviews.FirstOrDefault(reviews => reviews.ReviewId == id);
      _db.Reviews.Remove(thisReview);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBeer(int joinId)
    {
        var joinEntry = _db.BeerRatings.FirstOrDefault(entry => entry.BeerRatingId == joinId);
        _db.BeerRatings.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}