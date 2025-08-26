using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class MappingCustodian
    {
        [Key]
        public int Id { get; set; }        
        public int CustodiansId { get; set; }       
        public int TypeId { get; set; }
        public int ValueTypeId { get; set; }
        public bool IsActive { get; set; } = true;
       
    }
}
