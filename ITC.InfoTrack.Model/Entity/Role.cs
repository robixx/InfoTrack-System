using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }                 // Primary Key
        public int ViewOrder { get; set; }               
        public string RoleName { get; set; }            // Required, Unique
        public string Description { get; set; }        // Optional
        public bool IsActive { get; set; }      // Default TRUE
        public DateTime CreatedAt { get; set; }         // Default CURRENT_TIMESTAMP
        public DateTime UpdatedAt { get; set; }
    }
}
