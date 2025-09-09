using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class LevelSetting
    {
        [Key]
        public long LevelId { get; set; }

        [Required]
        [MaxLength(250)]
        public string LevelName { get; set; }

        public int? ParentId { get; set; }

        public int IsActive { get; set; } = 0;

        public int OrderView { get; set; }

        public int PropertyId { get; set; }
        public int Status { get; set; }
        public int IsType { get; set; }
    }
}
