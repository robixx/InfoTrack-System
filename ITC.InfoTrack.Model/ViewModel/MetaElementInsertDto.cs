using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class MetaElementInsertDto
    {
        public int  metaId { get; set; }
        public string element { get; set; }
        public string  description { get; set; }
        public bool isActive { get; set; }
    }
}
