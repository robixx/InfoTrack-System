using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class CustomTypeWiseLoadedDto
    {
        public long Id { get; set; }          // bigint -> long
        public string Name { get; set; }      // character varying -> string
        public long OrgId { get; set; }       // bigint -> long
        public long TypeId { get; set; }      // bigint -> long
    }
}
