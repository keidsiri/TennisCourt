using Microsoft.AspNetCore.Mvc;

namespace TennisCourt.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}