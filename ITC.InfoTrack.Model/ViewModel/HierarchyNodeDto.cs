using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class HierarchyNodeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }       
        public int? ParentId { get; set; }
        public int PropertyId { get; set; }
        public List<HierarchyNodeDto> Children { get; set; } = new();
    }
}
