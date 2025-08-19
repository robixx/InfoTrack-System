using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class RoleBaseSubMenuDto
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        public string MenuName { get; set; }      
        public int IsAllowed { get; set; }
        public int ViewOrder { get; set; }
    }
}
