using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TennisCourt.Models;
using System.Collections.Generic;
using System.Linq;

namespace TennisCourt.Controllers
{
  public class PlayersController : Controller
  {
    private readonly TennisCourtContext _db;

    public PlayersController(TennisCourtContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Players.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CourtId = new SelectList(_db.Courts, "CourtId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Player player, int CourtId)
    {
      _db.Players.Add(player);
      _db.SaveChanges();
      if (CourtId != 0)
      {
        _db.CourtPlayer.Add(new CourtPlayer() { CourtId = CourtId, PlayerId = player.PlayerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPlayer = _db.Players
          .Include(player => player.JoinEntities)
          .ThenInclude(join => join.Court)
          .FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    public ActionResult Edit(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.CourtId = new SelectList(_db.Courts, "CourtId", "Name");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult Edit(Player player, int CourtId)
    {
      if (CourtId != 0)
      {
        _db.CourtPlayer.Add(new CourtPlayer() { CourtId = CourtId, PlayerId = player.PlayerId });
      }
      _db.Entry(player).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCourt(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.CId = new SelectList(_db.Courts, "CourtId", "Name");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult AddCourt(Player player, int CourtId)
    {
      if (CourtId != 0)
      {
      _db.CourtPlayer.Add(new CourtPlayer() { CourtId = CourtId, PlayerId = player.PlayerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(item => item.ItemId == id);
      return View(thisPlayer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisplayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      _db.Players.Remove(thisPlayer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCourt(int joinId)
    {
      var joinEntry = _db.CourtPlayer.FirstOrDefault(entry => entry.CourtPlayerId == joinId);
      _db.CourtPlayer.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }    
}