using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class CheckListAnsBank
    {
        [Key]
        public int AnsBankId { get; set; }       // Primary Key
        public int CheckListId { get; set; }     // Foreign Key to CheckListBank
        public int AnsTypeId { get; set; }       // Type of the answer (e.g., Yes/No, Text, etc.)
        public string AnsValue { get; set; }     // Actual answer or value
        public int AnsStatus { get; set; }       
    }
}
