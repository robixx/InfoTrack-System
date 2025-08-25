using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class TokenDetails
    {
        [Key]
        public int Id { get; set; }
        public int TokenId { get; set; }
        public int CategoryId { get; set; }
        public int CategoryWiseId { get; set; }
        public string Comments { get; set; }
        public string DataProperty { get; set; }
    }
}
