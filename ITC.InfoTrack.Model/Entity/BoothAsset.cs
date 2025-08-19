using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("Booth_Asset")]
    public class BoothAsset
    {
        public int AssetId { get; set; }
        public int? BoothId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalType { get; set; }
        public int? TerminalStatus { get; set; }
        public DateTime? Createdate { get; set; }
        public int? Createdby { get; set; }
        public int? Updatedby { get; set; }
        public DateTime? Updateddate { get; set; }
    }
}
