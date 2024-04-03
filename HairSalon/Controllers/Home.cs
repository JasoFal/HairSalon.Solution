using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    private readonly HairSalonContext _db;

    public HomeController(HairSalonContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      Stylist[] stys = _db.Stylists.ToArray();
      Client[] clis = _db.Clients.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("stylists", stys);
      model.Add("clients", clis);
      return View(model);
    }
  }
}