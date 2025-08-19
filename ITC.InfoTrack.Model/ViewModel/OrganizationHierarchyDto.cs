using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class OrganizationHierarchyDto
    {
        public string Branch { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string SubBranch { get; set; } = string.Empty;
        public int SubBranchId { get; set; }
        public string District { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string Division { get; set; } = string.Empty;
        public int DivisionId { get; set; }
        public string Booth { get; set; } = string.Empty;
        public int BoothId { get; set; }
    }
}
