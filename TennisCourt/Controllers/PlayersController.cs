using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TennisCourt.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TennisCourt.Controllers
{ 
  [Authorize]
  public class PlayersController : Controller
  {
    private readonly TennisCourtContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public PlayersController(UserManager<ApplicationUser> userManager, TennisCourtContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userPlayers = _db.Players.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(_db.Players.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CourtId = new SelectList(_db.Courts, "CourtId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Player player, int CourtId)
    { var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      player.User = currentUser;
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
      ViewBag.CourtId = new SelectList(_db.Courts, "CourtId", "Name");
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
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
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