using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class RoleBasePagePermission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        public int MenuId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int? IsAllowed { get; set; } // Nullable to match the DB schema
    }
}
