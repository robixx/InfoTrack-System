using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class TreeNodeInsertDto
    {
        public int Id { get; set; } = 0;  
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string Address { get; set; } = string.Empty; 
        public string ContactNumber { get; set; } = string.Empty; 
        public int parentId {  get; set; } = 0;
        public List<TreeNodeInsertDto> Children { get; set; } = new List<TreeNodeInsertDto>();
    }
}
