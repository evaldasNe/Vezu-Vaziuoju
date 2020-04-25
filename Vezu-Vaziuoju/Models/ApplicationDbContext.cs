using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vezu_Vaziuoju.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Bonus> Bonus { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RatingReason> RatingReasons { get; set; }
        public virtual DbSet<RegistrationVerification> RegistrationVerifications { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripState> TripStates { get; set; }
        public virtual DbSet<UserBan> UserBans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.RegistrationVerifications)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserBans)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Drivers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Passengers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Admin>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.AdminId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Bonus)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.AdminId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.RegistrationVerifications)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.AdminId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.UserBans)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.AdminId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.AddressFrom)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.AddressTo)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.DriverId)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.AdminId)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Trips)
                .WithRequired(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bonus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Bonus>()
                .Property(e => e.AdminId)
                .IsUnicode(false);

            modelBuilder.Entity<Bonus>()
                .HasMany(e => e.Passengers)
                .WithOptional(e => e.Bonus)
                .HasForeignKey(e => e.BonusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.DriverId)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.PassengerId)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Driver)
                .HasForeignKey(e => e.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Driver)
                .HasForeignKey(e => e.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Passenger)
                .HasForeignKey(e => e.PassengerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Passenger)
                .HasForeignKey(e => e.PassengerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.PassengerId)
                .IsUnicode(false);

            modelBuilder.Entity<Rating>()
                .HasMany(e => e.RatingReasons)
                .WithRequired(e => e.Rating)
                .HasForeignKey(e => e.RatingId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RatingReason>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationVerification>()
                .Property(e => e.AdminId)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationVerification>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.PassengerId)
                .IsUnicode(false);

            modelBuilder.Entity<Trip>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Trip)
                .HasForeignKey(e => e.TripId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TripState>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TripState>()
                .HasMany(e => e.Trips)
                .WithRequired(e => e.TripState)
                .HasForeignKey(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserBan>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserBan>()
                .Property(e => e.AdminId)
                .IsUnicode(false);
        }
    }
}