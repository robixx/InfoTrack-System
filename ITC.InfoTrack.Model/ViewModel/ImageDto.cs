using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class ImageDto
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public string Branch { get; set; }
        public DateTime UploadDate { get; set; }
        public string Comments { get; set; }
    }
}
