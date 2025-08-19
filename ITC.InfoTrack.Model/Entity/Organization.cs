using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class Organization
    {
        [Key]
        public int OrgId { get; set; }               // Optional: if SERIAL or identity, it can be omitted during inserts
        public string OrgName { get; set; }
        public string OrgType { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public int InsertBy { get; set; }
        public DateTime InsertDate { get; set; }
        public int? UpdateBy { get; set; }           // Nullable in case no update yet
        public DateTime? UpdateDate { get; set; }    // Nullable in case no update yet
        public int OrgStatus { get; set; }
    }
}
