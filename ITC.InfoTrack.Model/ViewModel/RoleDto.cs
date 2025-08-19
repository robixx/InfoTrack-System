using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class RoleDto
    {
        public int RoleId { get; set; }                 // Primary Key       
        public string RoleName { get; set; }            // Required, Unique
        public string Description { get; set; }        // Optional
        public bool IsActive { get; set; }      // Default TRUE
        public int ViewOrder { get; set; }
    }
}
