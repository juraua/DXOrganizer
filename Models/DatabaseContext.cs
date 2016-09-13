using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXOrganizer.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext()
            : base("DatabaseConnection")
        {
            Database.SetInitializer<DatabaseContext>(new OrganizerDbInitializer());
        }

        public virtual DbSet<AlarmClock> AlarmClocks { get; set; }

        public virtual DbSet<AlarmStatusImage> AlarmStatusImages { get; set; }

        public virtual DbSet<Melody> Melodies { get; set; }
        
        public virtual DbSet<TextNote> TextNotes { get; set; }
        
        public virtual DbSet<EFAppointment> MyAppointments { get; set; }
        public virtual DbSet<EFResource> MyResources { get; set; }
    }
}
