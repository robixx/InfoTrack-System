using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IDropDown
    {
        Task<List<DropDownDto>> getOfficeList();
        Task<List<DropDownDto>> getBranchList(long OfficeId);
        Task<List<DropDownDtos>> getboothList(long BranchId);
        Task<List<DropDownDtos>> getAssetList(int BoothId);
        Task<List<DropDownDtos>> GetMetaGroupTitle();
        Task<List<DropDownDtos>> GetMetaParentTitle();
        Task<List<DropDownDto>> GetUser();
        Task<List<DropDownDto>> GetRole();
        Task<List<DropDownDtos>> GetOrganization();
        Task<List<DropDownDtos>> GetLevelSetting();
        Task<List<DropDownDtos>> GetOrganizationLocation();
        Task<List<DropDownDto>> GetCustomTypeWiseLocation( long OrgId, long TypeId);
        Task<(List<DropDownDtos> data, bool status)> getRootMetaPropertyElement();
        Task<(List<DropDownDtos> data, bool status)> getRootPropertyElement( int type);
        Task<(List<DropDownDto> data, bool status)> getTypePropertyElement();
        Task<(List<DropDownDto> data, bool status)> getLevelRootPropertyElement( int type);
        Task<List<DropDownDtos>> getChildenPropertyElement( int type);
    }
}
