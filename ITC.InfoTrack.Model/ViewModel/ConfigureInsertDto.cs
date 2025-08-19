using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ConfigureInsertDto
    {
        public int SetupId { get; set; }

        public string Type { get; set; }
        public int NameId { get; set; }

        public int? ParentId { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
