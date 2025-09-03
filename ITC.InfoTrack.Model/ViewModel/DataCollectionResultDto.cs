using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class DataCollectionResultDto
    {
        public long DataId { get; set; }
        public string ReferenceData { get; set; }
        public DateTime CollectionDate { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int? SourceId { get; set; }
        public string SourceName { get; set; }
        public int? LocationId { get; set; }
        public string Address { get; set; }
    }
}
