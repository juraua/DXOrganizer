namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmClock_Model_Changed_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlarmClocks", "AlarmClockName", c => c.String(nullable: false));
            AddColumn("dbo.Melodies", "MelodyName", c => c.String());
            DropColumn("dbo.AlarmClocks", "Name");
            DropColumn("dbo.Melodies", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Melodies", "Name", c => c.String());
            AddColumn("dbo.AlarmClocks", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Melodies", "MelodyName");
            DropColumn("dbo.AlarmClocks", "AlarmClockName");
        }
    }
}
