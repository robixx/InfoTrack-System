using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class LocationShowDto
    {
        public int LocationId {  get; set; }
        public string Organization {  get; set; }
        public string LocationTypeName {  get; set; }
        public string BranchName {  get; set; }
        public string LocationAddress { get; set; }
        public DateTime CreateDate { get; set; }
        public int IsActive { get; set; }
    }
}
