namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Advert",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AddressFrom = c.String(nullable: false, maxLength: 255, unicode: false),
                        AddressTo = c.String(nullable: false, maxLength: 255, unicode: false),
                        StartTime = c.DateTime(nullable: false, storeType: "date"),
                        TotalSeats = c.Int(nullable: false),
                        AvailableSeats = c.Int(nullable: false),
                        TicketPrice = c.Double(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        DriverId = c.String(nullable: false, maxLength: 255, unicode: false),
                        AdminId = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Driver", t => t.DriverId)
                .ForeignKey("dbo.Admin", t => t.AdminId)
                .Index(t => t.DriverId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Text = c.String(nullable: false, maxLength: 255, unicode: false),
                        AdvertId = c.Int(nullable: false),
                        DriverId = c.String(nullable: false, maxLength: 255, unicode: false),
                        PassengerId = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Driver", t => t.DriverId)
                .ForeignKey("dbo.Advert", t => t.AdvertId)
                .Index(t => t.AdvertId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Driver",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        Birthdate = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Passenger",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        BonusId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonus", t => t.BonusId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BonusId);
            
            CreateTable(
                "dbo.Bonus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        ValidFrom = c.DateTime(nullable: false, storeType: "date"),
                        ValidTo = c.DateTime(nullable: false, storeType: "date"),
                        AdminId = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admin", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        PassengerId = c.String(nullable: false, maxLength: 255, unicode: false),
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .ForeignKey("dbo.Passenger", t => t.PassengerId)
                .Index(t => t.PassengerId)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.RatingReason",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 255, unicode: false),
                        RatingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rating", t => t.RatingId)
                .Index(t => t.RatingId);
            
            CreateTable(
                "dbo.Trip",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false, storeType: "date"),
                        EndTime = c.DateTime(nullable: false, storeType: "date"),
                        EndedByDriver = c.Boolean(nullable: false),
                        State = c.Int(nullable: false),
                        AdvertId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TripState", t => t.State)
                .ForeignKey("dbo.Advert", t => t.AdvertId)
                .Index(t => t.State)
                .Index(t => t.AdvertId);
            
            CreateTable(
                "dbo.TripState",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ValidTill = c.DateTime(nullable: false, storeType: "date"),
                        IsUsed = c.Boolean(nullable: false),
                        PassengerId = c.String(nullable: false, maxLength: 255, unicode: false),
                        AdvertId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Passenger", t => t.PassengerId)
                .ForeignKey("dbo.Advert", t => t.AdvertId)
                .Index(t => t.PassengerId)
                .Index(t => t.AdvertId);
            
            CreateTable(
                "dbo.RegistrationVerification",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        AdminId = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Admin", t => t.AdminId)
                .Index(t => t.AdminId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserBan",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        From = c.DateTime(nullable: false, storeType: "date"),
                        To = c.DateTime(nullable: false, storeType: "date"),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AdminId = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Admin", t => t.AdminId)
                .Index(t => t.UserId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserBan", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.RegistrationVerification", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.Bonus", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.Advert", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.Trip", "AdvertId", "dbo.Advert");
            DropForeignKey("dbo.Ticket", "AdvertId", "dbo.Advert");
            DropForeignKey("dbo.Comment", "AdvertId", "dbo.Advert");
            DropForeignKey("dbo.UserBan", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RegistrationVerification", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Passenger", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ticket", "PassengerId", "dbo.Passenger");
            DropForeignKey("dbo.Rating", "PassengerId", "dbo.Passenger");
            DropForeignKey("dbo.Trip", "State", "dbo.TripState");
            DropForeignKey("dbo.Rating", "TripId", "dbo.Trip");
            DropForeignKey("dbo.RatingReason", "RatingId", "dbo.Rating");
            DropForeignKey("dbo.Passenger", "BonusId", "dbo.Bonus");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Driver", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Admin", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "DriverId", "dbo.Driver");
            DropForeignKey("dbo.Advert", "DriverId", "dbo.Driver");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserBan", new[] { "AdminId" });
            DropIndex("dbo.UserBan", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.RegistrationVerification", new[] { "UserId" });
            DropIndex("dbo.RegistrationVerification", new[] { "AdminId" });
            DropIndex("dbo.Ticket", new[] { "AdvertId" });
            DropIndex("dbo.Ticket", new[] { "PassengerId" });
            DropIndex("dbo.Trip", new[] { "AdvertId" });
            DropIndex("dbo.Trip", new[] { "State" });
            DropIndex("dbo.RatingReason", new[] { "RatingId" });
            DropIndex("dbo.Rating", new[] { "TripId" });
            DropIndex("dbo.Rating", new[] { "PassengerId" });
            DropIndex("dbo.Bonus", new[] { "AdminId" });
            DropIndex("dbo.Passenger", new[] { "BonusId" });
            DropIndex("dbo.Passenger", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Driver", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "DriverId" });
            DropIndex("dbo.Comment", new[] { "AdvertId" });
            DropIndex("dbo.Advert", new[] { "AdminId" });
            DropIndex("dbo.Advert", new[] { "DriverId" });
            DropIndex("dbo.Admin", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserBan");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.RegistrationVerification");
            DropTable("dbo.Ticket");
            DropTable("dbo.TripState");
            DropTable("dbo.Trip");
            DropTable("dbo.RatingReason");
            DropTable("dbo.Rating");
            DropTable("dbo.Bonus");
            DropTable("dbo.Passenger");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Driver");
            DropTable("dbo.Comment");
            DropTable("dbo.Advert");
            DropTable("dbo.Admin");
        }
    }
}
