using ITC.InfoTrack.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class OrgNodeDto
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public int? ParentId { get; set; }
        public int IsActive { get; set; }
        public int? OrderView { get; set; }
        public int? Status { get; set; }
        public int? PropertyId { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public List<OrgNodeDto> Children { get; set; } = new();
    }
}
