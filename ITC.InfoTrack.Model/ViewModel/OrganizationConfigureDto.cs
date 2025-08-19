using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class OrganizationConfigureDto
    {
        public int Id { get; set; }

        public int SetupId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int? ParentId { get; set; } // Nullable, since root nodes have null ParentId

        public int Level { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
