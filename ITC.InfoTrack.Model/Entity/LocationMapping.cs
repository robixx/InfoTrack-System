using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class LocationMapping
    {
        [Key]        
        public int Id { get; set; }

        public int LocationId { get; set; }  // Nullable to allow top-level locations

        public int TypeId { get; set; }      // Optional, depending on your business logic

        public int LocationType { get; set; } // Could be enum in C# for better readability

        public int IsActive { get; set; } = 1;  // Default to 1 (active)
    }
}
