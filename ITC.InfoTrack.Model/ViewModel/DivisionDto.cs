using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class DivisionDto
    {
        public string DivisionName { get; set; }
        public Dictionary<string, List<ImageDto>> Branches { get; set; }
    }
}
