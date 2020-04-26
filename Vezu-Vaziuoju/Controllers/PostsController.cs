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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts 
       public ActionResult Index(string addressFrom, string addressTo, DateTime? startTime)
        {
            var posts = (addressFrom != null && addressTo != null && startTime != null) ?
                db.Posts.Include(p => p.Admin).Include(p => p.Driver).Where(p => p.AddressFrom == addressFrom).Where(p => p.AddressTo == addressTo).Where(p => p.StartTime == startTime) :
                db.Posts.Include(p => p.Admin).Include(p => p.Driver);

            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        [Authorize(Roles = "Admin,Driver,Passenger")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin,Driver")]
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserId");
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserId");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Driver")]
        public ActionResult Create([Bind(Include = "Id,AddressFrom,AddressTo,StartTime,TotalSeats,AvailableSeats,TicketPrice,IsValid,DriverId,AdminId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserId", post.AdminId);
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserId", post.DriverId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserId", post.AdminId);
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserId", post.DriverId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AddressFrom,AddressTo,StartTime,TotalSeats,AvailableSeats,TicketPrice,IsValid,DriverId,AdminId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "UserId", post.AdminId);
            ViewBag.DriverId = new SelectList(db.Drivers, "Id", "UserId", post.DriverId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin,Driver")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Admin,Driver")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Posts/BuyTicket/5
        [Authorize(Roles = "Passenger")]
        public ActionResult BuyTicket(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else if (post.AvailableSeats <= 0)
            {
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // POST: Posts/BuyTicketPost/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Passenger")]
        public ActionResult BuyTicketPost(int id)
        {
            Post post = db.Posts.Find(id);
            var userId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id;

            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket
                {
                    Id = int.Parse(id.ToString() + post.AvailableSeats.ToString()),
                    Price = post.TicketPrice,
                    ValidTill = DateTime.Now.AddHours(5),
                    IsUsed = false,
                    PassengerId = userId,
                    PostId = post.Id,
                };
                db.Tickets.Add(ticket);
                post.AvailableSeats--;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return RedirectToAction("BuyTicket", new { id = id });
        }
    }
}
