using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class TokenDetailsShowDto
    {
        public int TokenId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryWiseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string DataProperty { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
    }
}
