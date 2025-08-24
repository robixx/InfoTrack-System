using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int UpdateBy { get; set; }

        public DateTime Updated { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
