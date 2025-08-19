using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ScheduleDataDto
    {
        public long CorpId { get; set; }
        public string CorpName { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchEmail { get; set; }
        public int BoothId { get; set; }
        public string BoothName { get; set; }
        public string BoothType { get; set; }
        public int AssetId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalType { get; set; }
        public DateTime Createdate { get; set; }
    }
}
