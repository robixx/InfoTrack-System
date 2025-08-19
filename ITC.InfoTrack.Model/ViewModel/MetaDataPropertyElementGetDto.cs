using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class MetaDataPropertyElementGetDto
    {
        public int DataElementId { get; set; }
        public string MetaElement { get; set; }
        public string PropertyName { get; set; }
        public string ParentData { get; set; }
        public int ViewOrder { get; set; }
        public bool isActive { get; set; }
    }
}
