namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAdvertToPost : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Advert", newName: "Post");
            RenameColumn(table: "dbo.Comment", name: "AdvertId", newName: "PostId");
            RenameColumn(table: "dbo.Ticket", name: "AdvertId", newName: "PostId");
            RenameColumn(table: "dbo.Trip", name: "AdvertId", newName: "PostId");
            RenameIndex(table: "dbo.Trip", name: "IX_AdvertId", newName: "IX_PostId");
            RenameIndex(table: "dbo.Comment", name: "IX_AdvertId", newName: "IX_PostId");
            RenameIndex(table: "dbo.Ticket", name: "IX_AdvertId", newName: "IX_PostId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ticket", name: "IX_PostId", newName: "IX_AdvertId");
            RenameIndex(table: "dbo.Comment", name: "IX_PostId", newName: "IX_AdvertId");
            RenameIndex(table: "dbo.Trip", name: "IX_PostId", newName: "IX_AdvertId");
            RenameColumn(table: "dbo.Trip", name: "PostId", newName: "AdvertId");
            RenameColumn(table: "dbo.Ticket", name: "PostId", newName: "AdvertId");
            RenameColumn(table: "dbo.Comment", name: "PostId", newName: "AdvertId");
            RenameTable(name: "dbo.Post", newName: "Advert");
        }
    }
}
