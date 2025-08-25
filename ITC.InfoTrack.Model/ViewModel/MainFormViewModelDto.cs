using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class MainFormViewModelDto
    {
        public string DateValue { get; set; }
        public string TypeId { get; set; }
        public string TypeText { get; set; }
        public string DistrictId { get; set; }
        public string DivisionId { get; set; }
        public string TypeNameId { get; set; }
        public string TypeNameText { get; set; }
        public List<TableRowViewModelDto> Items { get; set; }
    }
}
