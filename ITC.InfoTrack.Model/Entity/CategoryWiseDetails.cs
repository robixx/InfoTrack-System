using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("CategoryWise_Details")]
    public class CategoryWiseDetails
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int UpdateBy { get; set; }

        public DateTime Updated { get; set; }

        public DateTime ActiveDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
