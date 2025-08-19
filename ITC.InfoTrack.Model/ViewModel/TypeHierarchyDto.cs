using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class TypeHierarchyDto
    {
        public int TypeId { get; set; }
        public int? ParentTypeId { get; set; }
    }
}
