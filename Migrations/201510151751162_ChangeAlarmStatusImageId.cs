namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAlarmStatusImageId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages");
            DropPrimaryKey("dbo.AlarmStatusImages");
            AlterColumn("dbo.AlarmStatusImages", "AlarmStatusImageId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AlarmStatusImages", "AlarmStatusImageId");
            AddForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages", "AlarmStatusImageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages");
            DropPrimaryKey("dbo.AlarmStatusImages");
            AlterColumn("dbo.AlarmStatusImages", "AlarmStatusImageId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AlarmStatusImages", "AlarmStatusImageId");
            AddForeignKey("dbo.AlarmClocks", "AlarmStatusImageId", "dbo.AlarmStatusImages", "AlarmStatusImageId", cascadeDelete: true);
        }
    }
}
