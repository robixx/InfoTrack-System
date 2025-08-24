using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class CategoryWiseDataDAO : ICategoryData
    {
        private readonly DatabaseConnection _connection;

        public CategoryWiseDataDAO( DatabaseConnection connection)
        {
                _connection=connection;
        }

        public async Task<List<CategoryWiseDetailsDto>> getCategoryWiseData()
        {
            try
            {
                var result = await (from a in  _connection.CategoryWiseDetails 
                    join b in _connection.Category on a.CategoryId equals b.CategoryId
                    where b.IsActive==true
                    select new CategoryWiseDetailsDto
                    {
                        Id=a.Id,
                        CategoryId= a.CategoryId,
                        CategoryName=b.CategoryName,
                        Title=a.Title,
                        CreateBy=a.CreateBy,
                        CreateDate=a.CreateDate,
                        IsActive=a.IsActive,
                        ActiveDate=a.ActiveDate,
                    }
                    
                    ).ToListAsync();
                return result;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> SaveCategoryData(int categoryId, string Title, int loginuserId)
        {
            try
            {
                var iteam = new CategoryWiseDetails()
                {
                    CategoryId = categoryId,
                    Title = Title,
                    CreateBy = loginuserId,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    ActiveDate = DateTime.Now.AddYears(5),
                   

                };
                await _connection.CategoryWiseDetails.AddRangeAsync(iteam);
                await _connection.SaveChangesAsync();
                return ("Category Title Save Successfully", true);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
