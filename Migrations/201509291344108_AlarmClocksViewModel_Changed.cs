namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmClocksViewModel_Changed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmClocks",
                c => new
                    {
                        AlarmClockId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Start = c.Time(nullable: false, precision: 7),
                        Repeat = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Duration = c.Int(nullable: false),
                        DaysOfWeekDb = c.String(),
                        MelodyId = c.Int(),
                    })
                .PrimaryKey(t => t.AlarmClockId)
                .ForeignKey("dbo.Melodies", t => t.MelodyId)
                .Index(t => t.MelodyId);
            
            CreateTable(
                "dbo.Melodies",
                c => new
                    {
                        MelodyId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        MelodyInstance = c.Binary(),
                    })
                .PrimaryKey(t => t.MelodyId);
            
            CreateTable(
                "dbo.EFAppointments",
                c => new
                    {
                        UniqueID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AllDay = c.Boolean(nullable: false),
                        Subject = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Label = c.Int(nullable: false),
                        ResourceIDs = c.String(),
                        ReminderInfo = c.String(),
                        RecurrenceInfo = c.String(),
                    })
                .PrimaryKey(t => t.UniqueID);
            
            CreateTable(
                "dbo.EFResources",
                c => new
                    {
                        UniqueID = c.Int(nullable: false, identity: true),
                        ResourceID = c.Int(nullable: false),
                        ResourceName = c.String(),
                        Color = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueID);
            
            CreateTable(
                "dbo.TextNotes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        Color = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        Font = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateComplet = c.Boolean(nullable: false),
                        ViewType = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId);
            
            CreateTable(
                "dbo.Icons",
                c => new
                    {
                        IconId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IconImage = c.Binary(),
                        TextNote_NoteId = c.Int(),
                    })
                .PrimaryKey(t => t.IconId)
                .ForeignKey("dbo.TextNotes", t => t.TextNote_NoteId)
                .Index(t => t.TextNote_NoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Icons", "TextNote_NoteId", "dbo.TextNotes");
            DropForeignKey("dbo.AlarmClocks", "MelodyId", "dbo.Melodies");
            DropIndex("dbo.Icons", new[] { "TextNote_NoteId" });
            DropIndex("dbo.AlarmClocks", new[] { "MelodyId" });
            DropTable("dbo.Icons");
            DropTable("dbo.TextNotes");
            DropTable("dbo.EFResources");
            DropTable("dbo.EFAppointments");
            DropTable("dbo.Melodies");
            DropTable("dbo.AlarmClocks");
        }
    }
}
