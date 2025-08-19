using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IMetaData
    {
        Task<(string message, bool status)> SaveMetaElement(MetaElementInsertDto model);
        Task<(string message, bool status)> SaveMetaElementProperty(MetaDataPropertyElementInsertDto model);
        Task<List<MetaElementInsertDto>> getMetaElement();
        Task<(MetaElementInsertDto data, bool status)> EditMetaElement(int metaId);
        Task<(MetaDataPropertyElementInsertDto data, bool status)> EditMetaPropertyElement(int dataElemetId);
        Task<List<MetaDataPropertyElementGetDto>> getMetaPropertyElement();
        


    }
}
