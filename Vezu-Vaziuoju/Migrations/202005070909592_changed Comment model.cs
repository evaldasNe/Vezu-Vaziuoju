namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedCommentmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "DriverId", "dbo.Driver");
            DropIndex("dbo.Comment", new[] { "DriverId" });
            AddColumn("dbo.Comment", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comment", "Date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Comment", "UserId");
            AddForeignKey("dbo.Comment", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comment", "DriverId");
            DropColumn("dbo.Comment", "PassengerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "PassengerId", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AddColumn("dbo.Comment", "DriverId", c => c.String(nullable: false, maxLength: 255, unicode: false));
            DropForeignKey("dbo.Comment", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comment", new[] { "UserId" });
            AlterColumn("dbo.Comment", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Comment", "UserId");
            CreateIndex("dbo.Comment", "DriverId");
            AddForeignKey("dbo.Comment", "DriverId", "dbo.Driver", "Id");
        }
    }
}
