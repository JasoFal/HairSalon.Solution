using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Client> model = _db.Clients.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      if (!ModelState.IsValid)
      {
        return RedirectToAction("Create");
      }
      else
      {
        _db.Clients.Add(client);
        _db.SaveChanges();
        return RedirectToAction("Index", "Stylists");
      }
    }

    public ActionResult Details(int id)
    {
      Client thisClient = _db.Clients
        .Include(c => c.JoinEntities)
        .ThenInclude(j => j.Stylist)
        .FirstOrDefault(c => c.ClientId == id);
      return View(thisClient);
    }

    public ActionResult Edit(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(c => c.ClientId == id);
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Clients.Update(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(c => c.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(c => c.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index", "Stylists");
    }

    public ActionResult AddStylist(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(c => c.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistLastName");
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult AddStylist(Client client, int stylistId)
    {
      #nullable enable
      ClientStylist? joinEntity = _db.ClientStylists.FirstOrDefault(j => (j.StylistId == stylistId && j.ClientId == client.ClientId));
      #nullable disable
      if (joinEntity == null && stylistId != 0)
      {
        _db.ClientStylists.Add(new ClientStylist() { StylistId = stylistId, ClientId = client.ClientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = client.ClientId });
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