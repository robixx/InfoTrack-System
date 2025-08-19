using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class MetaDataType
    {
        [Key]
        public int PropertyId { get; set; }            // Primary Key
        public string PropertyName { get; set; }       // Name of the metadata property
        public string Description { get; set; }       // Name of the metadata property
        public int PropertyStatus { get; set; }
        public int RootValue { get; set; }
        public int Label { get; set; }
    }
}
