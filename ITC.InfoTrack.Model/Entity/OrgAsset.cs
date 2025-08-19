using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class OrgAsset
    {
        [Key]
        public int AssetId { get; set; }                     // Primary Key
        public int OrgId { get; set; }                       // Foreign Key to Organization
        public string AssetDescription { get; set; }         // Description of the asset
        public string AssetTag { get; set; }                 // Tag or identifier for the asset
        public int AssetTypeId { get; set; }                 // Type ID for categorizing the asset
        public int LocationId { get; set; }                  // Location of the asset
        public int InsertBy { get; set; }                    // ID of user who inserted the record
        public DateTime InsertDate { get; set; }             // Timestamp when the asset was inserted
        public int? UpdateBy { get; set; }                   // ID of user who last updated the record (nullable)
        public DateTime? UpdateDate { get; set; }            // Timestamp of the last update (nullable)
        public int AssetStatus { get; set; }
    }
}
