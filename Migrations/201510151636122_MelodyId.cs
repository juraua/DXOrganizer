namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MelodyId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlarmClocks", "MelodyId", "dbo.Melodies");
            DropPrimaryKey("dbo.Melodies");
            AlterColumn("dbo.Melodies", "MelodyId", c => c.Int(nullable: false, identity: false));
            AlterColumn("dbo.Melodies", "Name", c => c.String());
            AddPrimaryKey("dbo.Melodies", "MelodyId");
            AddForeignKey("dbo.AlarmClocks", "MelodyId", "dbo.Melodies", "MelodyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlarmClocks", "MelodyId", "dbo.Melodies");
            DropPrimaryKey("dbo.Melodies");
            AlterColumn("dbo.Melodies", "Name", c => c.Int(nullable: false));
            AlterColumn("dbo.Melodies", "MelodyId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Melodies", "MelodyId");
            AddForeignKey("dbo.AlarmClocks", "MelodyId", "dbo.Melodies", "MelodyId");
        }
    }
}
