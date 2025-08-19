using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class OrgResource
    {
        [Key]
        public int ResourceProfileId { get; set; }      // Primary Key (Auto-increment if SERIAL)
        public int OrgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int ResourceTypeId { get; set; }
        public int InsertBy { get; set; }
        public DateTime InsertDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
