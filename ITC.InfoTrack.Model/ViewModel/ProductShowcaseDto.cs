using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ProductShowcaseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public string Comments { get; set; } = string.Empty;
    }
}
