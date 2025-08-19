using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class ResourceList
    {
        [Key]
        public int ResListId { get; set; }               // Primary Key
        public int ResourceProfileId { get; set; }       // Foreign Key to OrgResource or similar
        public int InsertBy { get; set; }                // User ID who inserted
        public DateTime InsertDate { get; set; }         // Insertion timestamp
        public int ResourceStatus { get; set; }
    }
}
