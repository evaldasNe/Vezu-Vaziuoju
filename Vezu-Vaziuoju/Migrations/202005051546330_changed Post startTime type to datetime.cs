namespace Vezu_Vaziuoju.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPoststartTimetypetodatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "StartTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "StartTime", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
