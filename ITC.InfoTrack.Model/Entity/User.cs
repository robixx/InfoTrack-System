using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class User
    {
        [Key] 
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string LoginName { get; set; }
        public string EncriptedPassword { get; set; }
        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastPasswordChange { get; set; }

        public int? UserStatus { get; set; }

        public long PoolId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
