using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class TokenMasterDto
    {
        public int TokenId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string Address { get; set; }
        public DateTime TokenDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
