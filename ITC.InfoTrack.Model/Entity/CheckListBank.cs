using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class CheckListBank
    {
        [Key]
        public int CheckListId { get; set; }           // Primary Key
        public string CheckListItem { get; set; }      // Description or label of the checklist item
        public int CheckListStatus { get; set; }
    }
}
