using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class VisitSchedule
    {
        [Key]
        public int ScheduleId { get; set; }         // Primary Key
        public int AssignUserId { get; set; }              // Foreign Key to Organization
        public DateTime DateOfVisit { get; set; }   // Visit date
        public TimeSpan TimeOfVisit { get; set; }   // Visit time
        public int LocationId { get; set; }         // Foreign Key to Location
        public int TokenId { get; set; }         // Foreign Key to Location
        public int InsertBy { get; set; }           // User ID who created
        public DateTime InsertDate { get; set; }    // Creation timestamp
        public int? UpdateBy { get; set; }          // User ID who last updated
        public DateTime? UpdateDate { get; set; }   // Last update timestamp
        public int ScheduleStatus { get; set; }     // Status (e.g., active, cancelled)
        public int IsVisited { get; set; }     // Status (e.g., active, cancelled)
    }
}
