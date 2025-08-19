using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class InsertUserRoleDto
    {
        public int userId { get; set; }
        public int roleId { get; set; }
        public bool isActive { get; set; }
    }
}
