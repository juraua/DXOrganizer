using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DXOrganizer
{
    using System.Data.Entity;
    using System.Linq;

    public class EFResource
    {
        [Key()]
        public int UniqueID { get; set; }
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public int Color { get; set; }
    }
}