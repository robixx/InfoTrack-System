using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ScheduleEntryDto
    {
        public string LocationText { get; set; }      
        public string CorpId { get; set; }            
        public string BranchId { get; set; }          
        public string BoothId { get; set; }           
        public string AssetId { get; set; }           
        public string Date { get; set; }              
        public string Time { get; set; }              
        public string AssignedUser { get; set; }      
        public string Priority { get; set; }          
        public string Comments { get; set; }          
    }
}
