namespace AutomatedSchedulingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDatabases : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "RoleID_ID", "dbo.Roles");
            DropIndex("dbo.Employees", new[] { "RoleID_ID" });
            AddColumn("dbo.Employees", "Role", c => c.String());
            DropColumn("dbo.Employees", "RoleID_ID");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Employees", "RoleID_ID", c => c.Int());
            DropColumn("dbo.Employees", "Role");
            CreateIndex("dbo.Employees", "RoleID_ID");
            AddForeignKey("dbo.Employees", "RoleID_ID", "dbo.Roles", "ID");
        }
    }
}
