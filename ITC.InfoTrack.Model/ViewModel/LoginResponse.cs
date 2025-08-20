using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int  RoleId { get; set; }
    }
}
