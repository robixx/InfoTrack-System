using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class TableRowViewModelDto
    {
        public string Id { get; set; }
        public string SelectValue { get; set; }
        public string Comment { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
