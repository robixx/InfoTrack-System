using ITC.InfoTrack.Model.Entity;
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
        Task<List<CategoryWiseDetails>>getCategoryWiseData();
    }
}
