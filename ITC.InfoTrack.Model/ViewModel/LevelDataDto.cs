using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class LevelDataDto
    {
        public int TypeId { get; set; }
        public int? ParentId { get; set; }
        public string typeName { get; set; }
        public int OrderView { get; set; }
    }
}
