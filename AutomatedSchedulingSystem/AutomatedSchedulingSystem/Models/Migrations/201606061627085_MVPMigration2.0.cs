namespace AutomatedSchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MVPMigration20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Days", "Date", c => c.String());
            AlterColumn("dbo.Schedules", "StartDate", c => c.String());
            AlterColumn("dbo.Schedules", "EndDate", c => c.String());
            AlterColumn("dbo.Shifts", "StartTime", c => c.String());
            AlterColumn("dbo.Shifts", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shifts", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shifts", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Schedules", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Schedules", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Days", "Date", c => c.DateTime(nullable: false));
        }
    }
}
