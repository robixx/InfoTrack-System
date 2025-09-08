using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class DataMappingDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int SourceId { get; set; }
        public int DivisionId { get; set; }

        private string _sourceName;
        public string SourceName
        {
            get => string.IsNullOrEmpty(_sourceName)
                   ? _sourceName
                   : char.ToUpper(_sourceName[0]) + _sourceName.Substring(1).ToLower();
            set => _sourceName = value;
        }

        public string Address { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsActive { get; set; }

        public string ElementName { get; set; }
    }
}
