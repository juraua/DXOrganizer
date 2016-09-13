namespace DXOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlarmStatusImagesAndMelodiesDropIdentity : DbMigration
    {
        public override void Up()
        {
            SqlFile(Configuration.AssemblyDirectory + @"\Migrations\Sql\AlarmStatusImagesDropIdentity.sql");
            SqlFile(Configuration.AssemblyDirectory + @"\Migrations\Sql\MelodiesDropIdentity.sql");
        }
        
        public override void Down()
        {
        }
    }
}
