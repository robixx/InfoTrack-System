using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class InsertOrgDto
    {
        public int locationId { get; set; }
        public int orgId { get; set; }
        public int customId { get; set; }
        public int customTypeId { get; set; }
        public int locationType { get; set; }
        public string orgaddress { get; set; }
        public bool isActive { get; set; }
    }
}
