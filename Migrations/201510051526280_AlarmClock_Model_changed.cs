namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmClock_Model_changed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmStatusImages",
                c => new
                    {
                        AlarmStatusImageId = c.Int(nullable: false, identity: true),
                        AlarmStatusImageInstance = c.Binary(),
                    })
                .PrimaryKey(t => t.AlarmStatusImageId);
            
            AddColumn("dbo.AlarmClocks", "AlarmStatusImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.AlarmClocks", "AlarmStatusImageId");
            AddForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages", "AlarmStatusImageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages");
            DropIndex("dbo.AlarmClocks", new[] { "AlarmStatusImageId" });
            DropColumn("dbo.AlarmClocks", "AlarmStatusImageId");
            DropTable("dbo.AlarmStatusImages");
        }
    }
}
