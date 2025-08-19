using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class MetaDataElements
    {
        [Key]
        public int DataElementId { get; set; }         // Primary Key
        public int PropertyId { get; set; }            // Foreign Key to MetaDataType
        public int? ParentPropertyId { get; set; }     // Nullable, for hierarchical data
        public string MetaElementValue { get; set; }   // Display or stored value
        public int PropertyViewOrder { get; set; }     // Display order
        public int DataElementStatus { get; set; }     // Status (e.g., Active/Inactive)
    }
}
