using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("TokenDetails_Image")]
    public class TokenDetailsImage
    {
        [Key]
        public int ImageId { get; set; }
        public int TokenId { get; set; }
        public int CategoryId { get; set; }
        public int CategoryWiseId { get; set; }
        public string ImageName { get; set; }
        public int IsActive { get; set; }
    }
}
