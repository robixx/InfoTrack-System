using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class OrgRootConfiguration
    {
        [Key]
        public int Id { get; set; }

        public int DataElementId { get; set; }
        public int OrderView { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }  // nullable
        public int? IsActive { get; set; }   // nullable
    }
}
