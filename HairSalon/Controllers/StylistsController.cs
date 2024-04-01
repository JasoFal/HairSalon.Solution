using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Stylists.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist thisStylist = _db.Stylists
        .Include(s => s.JoinEntities)
        .ThenInclude(j => j.Client)
        .FirstOrDefault(s => s.StylistId == id);
      return View(thisStylist);
    }

    public ActionResult Edit(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(s => s.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Stylists.Update(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(s => s.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(s => s.StylistId == id);
      _db.Stylists.Remove(thisStylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddClient(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(s => s.StylistId == id);
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "ClientLastName");
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult AddClient(Stylist stylist, int clientId)
    {
      #nullable enable
      ClientStylist? joinEntity = _db.ClientStylists.FirstOrDefault(j => (j.ClientId == clientId && j.StylistId == stylist.StylistId));
      #nullable disable
      if (joinEntity == null && clientId != 0)
      {
        _db.ClientStylists.Add(new ClientStylist() { ClientId = clientId, StylistId = stylist.StylistId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = stylist.StylistId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      ClientStylist joinEntry = _db.ClientStylists.FirstOrDefault(e => e.ClientStylistId == joinId);
      _db.ClientStylists.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}