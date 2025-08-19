using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class MenuSetUp
    {
        [Key] 
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string RouteName { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int IsMainMenu { get; set; }
        public int ParentId { get; set; }
        public int IsActive { get; set; }
        public int ViewOrder { get; set; }
    }
}
