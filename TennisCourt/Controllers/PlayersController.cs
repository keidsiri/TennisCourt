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
  }    
}