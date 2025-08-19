using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class OrgLocation
    {
        [Key]
        public int LocationId { get; set; }                  // Primary Key
        public int OrgId { get; set; }                       // Foreign Key to Organization
        public string LocationAddress { get; set; }          // Address of the location
        public string LocationName { get; set; }             // Name of the location
        public int LocationTypeId { get; set; }              // Type of location
        public int AssignPropertyId { get; set; }       // Optional parent location
        public int InsertBy { get; set; }                    // ID of the user who inserted the record
        public DateTime InsertDate { get; set; }             // Timestamp of insertion
        public int? UpdateBy { get; set; }                   // ID of the user who last updated the record
        public DateTime? UpdateDate { get; set; }            // Timestamp of last update
        public int LocationStatus { get; set; }
    }
}
