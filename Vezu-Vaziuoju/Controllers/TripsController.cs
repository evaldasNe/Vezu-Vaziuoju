using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vezu_Vaziuoju;
using Vezu_Vaziuoju.Models;

namespace Vezu_Vaziuoju.Controllers
{
    [Authorize(Roles = "Admin,Driver,Passenger")]
    public class TripsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trips
        public ActionResult Index()
        {
            List<Trip> trips;

            if (User.IsInRole("Passenger"))
            {
                var passengerID = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id;

                trips = db.Trips.
                    Include(t => t.Post).
                    Include(t => t.TripState).
                    ToList().
                    Where(t => t.Post.Tickets.FirstOrDefault(v => v.PassengerId == passengerID) != null).ToList();

            }
            else if (User.IsInRole("Driver"))
            {
                trips = db.Trips.
                    Include(t => t.Post).
                    Include(t => t.TripState).
                    Where(t => t.Post.Driver.User.Email == User.Identity.Name).
                    ToList();
            }
            else
            {
                trips = db.Trips.Include(t => t.Post).Include(t => t.TripState).ToList();
            }
            
            return View(trips);
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "Id", "AddressFrom");
            ViewBag.State = new SelectList(db.TripStates, "Id", "Name");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime,EndTime,EndedByDriver,State,PostId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostId = new SelectList(db.Posts, "Id", "AddressFrom", trip.PostId);
            ViewBag.State = new SelectList(db.TripStates, "Id", "Name", trip.State);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "AddressFrom", trip.PostId);
            ViewBag.State = new SelectList(db.TripStates, "Id", "Name", trip.State);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime,EndedByDriver,State,PostId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "AddressFrom", trip.PostId);
            ViewBag.State = new SelectList(db.TripStates, "Id", "Name", trip.State);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Trips/Start/5
        public ActionResult Start(int id)
        {
            Trip trip = db.Trips.Find(id);

            if (trip.TripState.Name == "Nepradeta")
            {
                trip.StartTime = DateTime.Now;
                trip.State = 2;
                foreach (var ticket in trip.Post.Tickets)
                {
                    ticket.IsUsed = true;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Trips/End/5
        public ActionResult End(int id)
        {
            Trip trip = db.Trips.Find(id);

            if (trip.TripState.Name == "Vyksta")
            {
                trip.State = 3;
                trip.EndTime = DateTime.Now;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Progress(int id)
        {
            Trip trip = db.Trips.Find(id);
            return View(trip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
