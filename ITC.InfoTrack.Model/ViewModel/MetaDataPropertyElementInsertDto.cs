using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class MetaDataPropertyElementInsertDto
    {
        public int dataElementId { get; set; }
        public int groupId { get; set; }
        public int parentValueId { get; set; }
        public string propertyName { get; set; }
        public int orderview { get; set; }
        public bool isActive { get; set; }
    }
}
