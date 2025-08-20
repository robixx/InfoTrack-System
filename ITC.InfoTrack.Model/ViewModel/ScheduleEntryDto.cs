using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ScheduleEntryDto
    {
        public string LocationText { get; set; }      
        public string BranchId { get; set; }            
        public string SubBranchId { get; set; }          
        public string DistrictId { get; set; }          
        public string divisionId { get; set; }          
        public string BoothId { get; set; }         
               
        public string Date { get; set; }              
        public string Time { get; set; }              
        public string AssignedUser { get; set; }      
        public string Priority { get; set; }          
        public string Comments { get; set; }          
    }
}
