using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DXOrganizer.Models
{
    public class OrganizerDbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            //------------AlarmClockDbInitializer----------
            
            //------------TextNoteDbInitializer-----------
            
            //------------SchedulerDBInitializer----------
            IList<EFResource> defaultResources = new List<EFResource>();

            defaultResources.Add(new EFResource() { ResourceID = 1, ResourceName = "Resource 1" });
            defaultResources.Add(new EFResource() { ResourceID = 2, ResourceName = "Resource 2" });

            foreach (EFResource res in defaultResources)
                db.MyResources.Add(res);
            base.Seed(db);
        }
    }
}