namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedfewmodels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trip", "StartTime", c => c.DateTime());
            AlterColumn("dbo.Trip", "EndTime", c => c.DateTime());
            AlterColumn("dbo.Ticket", "ValidTill", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "ValidTill", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Trip", "EndTime", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Trip", "StartTime", c => c.DateTime(storeType: "date"));
        }
    }
}
