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
                db.Posts.Include(p => p.Admin).Include(p => p.Driver).Where(p => p.AddressFrom ==
                addressFrom).Where(p => p.AddressTo == addressTo).Where(p => p.StartTime == startTime) :
                db.Posts.Include(p => p.Admin).Include(p => p.Driver);

            if (!User.IsInRole("Admin")) 
            {
                posts = posts.Where(p => p.IsValid == true || p.Driver.User.Email == User.Identity.Name);
            }

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
            CommentViewModel model = new CommentViewModel{Post = post};
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Posts/Details/5
        [Authorize(Roles = "Admin,Driver,Passenger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, CommentViewModel model)
        {
            Post post = db.Posts.Find(id);

            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    Id = GenerateId(),
                    Date = DateTime.Now,
                    Text = model.Text,
                    PostId = id,
                    UserId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id,
                };

                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", id);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Driver")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Id = GenerateId(),
                    AddressFrom = model.AddressFrom,
                    AddressTo = model.AddressTo,
                    StartTime = model.StartTime,
                    TotalSeats = model.TotalSeats,
                    AvailableSeats = model.TotalSeats,
                    TicketPrice = model.TicketPrice,
                    IsValid = false,
                    DriverId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id
                };

                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private int GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt16(buffer, 0);
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
            PostViewModel model = new PostViewModel
            {
                AddressFrom = post.AddressFrom,
                AddressTo = post.AddressTo,
                StartTime = post.StartTime,
                TotalSeats = post.TotalSeats,
                TicketPrice = post.TicketPrice
            };
            return View(model);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostViewModel model)
        {
            Post post = db.Posts.Find(id);
            if (ModelState.IsValid)
            {
                post.AddressFrom = model.AddressFrom;
                post.AddressTo = model.AddressTo;
                post.StartTime = model.StartTime;
                post.TotalSeats = model.TotalSeats;
                post.AvailableSeats = model.TotalSeats;
                post.TicketPrice = model.TicketPrice;

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Posts/Delete/5
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
            db.Tickets.RemoveRange(post.Tickets);
            db.Comments.RemoveRange(post.Comments);
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
            string passengerId = db.Passengers.SingleOrDefault(u => u.User.Email == User.Identity.Name).Id;
            Ticket ticket = db.Tickets.SingleOrDefault(t => t.PassengerId == passengerId && t.PostId == post.Id);

            if (post == null)
            {
                return HttpNotFound();
            }
            else if (post.AvailableSeats <= 0 || post.IsValid == false || ticket != null)
            {
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // POST: Posts/BuyTicketPost/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Passenger")]
        public ActionResult BuyTicketPost(int id)
        {
            Post post = db.Posts.Find(id);
            var userId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id;

            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket
                {
                    Id = GenerateId(),
                    Price = post.TicketPrice,
                    ValidTill = post.StartTime.AddHours(5),
                    IsUsed = false,
                    PassengerId = userId,
                    PostId = post.Id,
                };
                bool IsFirstSale = (post.AvailableSeats == post.TotalSeats) ? true : false;
                db.Tickets.Add(ticket);
                post.AvailableSeats--;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                if (IsFirstSale)
                {
                    CreateTrip(post);
                }

                return RedirectToAction("TicketDetails", new { id = ticket.Id});
            }

            return RedirectToAction("BuyTicket", new { id = id });
        }

        private void CreateTrip(Post post)
        {
            Trip trip = new Trip
            {
                Id = GenerateId(),
                EndedByDriver = false,
                State = db.TripStates.SingleOrDefault(t => t.Id == 1).Id,
                PostId = post.Id
            };

            db.Trips.Add(trip);
            db.SaveChanges();
        }

        // GET: Posts/VerifyPost/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult VerifyPost(int id)
        {
            Post post = db.Posts.Find(id);
            var adminId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id;
            
            if (post.IsValid == false)
            {
                post.IsValid = true;
                post.AdminId = adminId;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Posts/TicketDetails/5
        [Authorize(Roles = "Passenger")]
        public ActionResult TicketDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            TicketViewModel model = new TicketViewModel { Ticket = ticket };
            Trip trip = ticket.Post.Trips.FirstOrDefault();
            if (trip != null && ticket.IsUsed == true)
            {
                var rate = trip.Ratings.SingleOrDefault(r => r.PassengerId == db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id);
                model.TripRate = (rate == null) ? 0 : rate.Rate;
                if (rate != null)
                {
                    var reason = db.RatingReasons.SingleOrDefault(r => r.RatingId == rate.Id);
                    model.RatingReasonText = (reason == null) ? null : reason.Text;
                }
            }
            return View(model);
        }

        // POST: Posts/TicketDetails/5
        [Authorize(Roles = "Passenger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TicketDetails(int id, TicketViewModel model)
        {
            Ticket ticket = db.Tickets.Find(id);

            if (ModelState.IsValid && ticket.IsUsed == true)
            {
                Rating rating = new Rating
                {
                    Id = GenerateId(),
                    PassengerId = db.Users.SingleOrDefault(u => u.Email == User.Identity.Name).Id,
                    Rate = model.TripRate,
                    TripId = db.Trips.SingleOrDefault(t => t.PostId == ticket.PostId).Id
                };

                if (model.RatingReasonText != null)
                {
                    RatingReason ratingReason = new RatingReason
                    {
                        Id = GenerateId(),
                        Text = model.RatingReasonText,
                        RatingId = rating.Id
                    };
                    db.RatingReasons.Add(ratingReason);
                }

                db.Ratings.Add(rating);
                db.SaveChanges();
            }
            return RedirectToAction("TicketDetails", id);
        }
    }
}
