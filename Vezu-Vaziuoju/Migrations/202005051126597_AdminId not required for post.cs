namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminIdnotrequiredforpost : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Post", new[] { "AdminId" });
            AlterColumn("dbo.Post", "AdminId", c => c.String(maxLength: 255, unicode: false));
            CreateIndex("dbo.Post", "AdminId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Post", new[] { "AdminId" });
            AlterColumn("dbo.Post", "AdminId", c => c.String(nullable: false, maxLength: 255, unicode: false));
            CreateIndex("dbo.Post", "AdminId");
        }
    }
}
