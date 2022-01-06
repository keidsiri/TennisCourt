using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TennisCourt.Models;
using System.Collections.Generic;
using System.Linq;

namespace TennisCourt.Controllers
{
  public class CourtsController : Controller
  {
    private readonly TennisCourtContext _db;

    public CourtsController(TennisCourtContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Court> model = _db.Courts.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Court court)
    {
      _db.Courts.Add(court);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCourt = _db.Courts
        .Include(court => court.JoinEntities)
        .ThenInclude(join => join.Item)
        .FirstOrDefault(court => court.CourtId == id);
      return View(thisCourt);
    }
    
    public ActionResult Edit(int id)
    {
      var thisCourt = _db.Courts.FirstOrDefault(court => court.CourtId == id);
      return View(thisCourt);
    }

    [HttpPost]
    public ActionResult Edit(Court court)
    {
      _db.Entry(court).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCourt = _db.Courts.FirstOrDefault(court => court.CourtId == id);
      return View(thisCourt);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCourt = _db.Courts.FirstOrDefault(court => court.CourtId == id);
      _db.Courts.Remove(thisCourt);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}