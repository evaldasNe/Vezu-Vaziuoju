namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTrip : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trip", "StartTime", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trip", "StartTime", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
