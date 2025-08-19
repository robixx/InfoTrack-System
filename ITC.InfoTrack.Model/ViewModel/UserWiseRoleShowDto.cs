using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ShowRolePageDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int IsActive { get; set; }
        public int ViewOrder { get; set; }
        public string Description { get; set; }
        public List<string> MenuData { get; set; }
    }
    public class UserWiseRoleShowDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int IsActive { get; set; }
        public int ViewOrder { get; set; }
        public string Description { get; set; }
    }

    
}
