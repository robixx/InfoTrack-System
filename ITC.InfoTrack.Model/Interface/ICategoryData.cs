using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface ICategoryData
    {
        Task<(string message, bool status)>SaveCategoryData(int categoryId, string Title, int loginuserId);
        Task<List<CategoryWiseDetailsDto>>getCategoryWiseData();
        Task<(string message, bool success)>SaveTokenGenerateData(MainFormViewModelDto model, int loginUserId);
        Task<List<TokenMasterDto>>getTokenData();
        Task<List<ProductShowcaseDto>>getTokenDetailsShow( int TokenId);

        Task<List<TokenMasterDto>> getTokenDataFilter(int? typeId, int? districtId, int? divisionId, int? ValueTypeId);


    }
}
