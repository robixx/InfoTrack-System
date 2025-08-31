using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class DataCollection
    {
        public long DataId { get; set; }  // bigint

        public string ReferenceData { get; set; }  // varchar(150)

        public DateTime CollectionDate { get; set; }  // timestamp (nullable)

        public int TypeId { get; set; }  // integer (nullable)

        public int DistrictId { get; set; }  // integer (nullable)

        public int DivisionId { get; set; }  // integer (nullable)

        public int SourceId { get; set; }  // integer (nullable)

        public int CreateBy { get; set; }  // integer (nullable)
        public int IsSchedule { get; set; }  // integer (nullable)

        public DateTime CreateAt { get; set; }  // timestamp (nullable)
    }
}
