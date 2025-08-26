using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql;
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
        private readonly string _imagePath;

        public CategoryWiseDataDAO(DatabaseConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _imagePath = configuration["ImageStorage:TokenImagePath"];
        }

        public async Task<List<CategoryWiseDetailsDto>> getCategoryWiseData()
        {
            try
            {
                var result = await (from a in _connection.CategoryWiseDetails
                                    join b in _connection.Category on a.CategoryId equals b.CategoryId
                                    where b.IsActive == true
                                    select new CategoryWiseDetailsDto
                                    {
                                        Id = a.Id,
                                        CategoryId = a.CategoryId,
                                        CategoryName = b.CategoryName,
                                        Title = a.Title,
                                        CreateBy = a.CreateBy,
                                        CreateDate = a.CreateDate,
                                        IsActive = a.IsActive,
                                        ActiveDate = a.ActiveDate,
                                    }

                    ).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TokenMasterDto>> getTokenData()
        {
            try
            {
                var list= await _connection.TokenMasterDto.FromSqlRaw("SELECT * FROM get_token_master_data()")
                        .ToListAsync();
                return list;
            }
            catch(Exception ex)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool success)> SaveTokenGenerateData(MainFormViewModelDto model, int loginUserId)
        {
            try
            {

                if (model == null)
                {

                    return ("Model Invaild Data", false);
                }
                var transaction = await _connection.Database.BeginTransactionAsync();

                var masterTb = new TokenMaster
                {
                    DistrictId = Convert.ToInt32(model.DistrictId),
                    DivisionId = Convert.ToInt32(model.DivisionId),
                    TypeId = Convert.ToInt32(model.TypeId),
                    SourceId = Convert.ToInt32(model.TypeNameId),
                    TokenDate = Convert.ToDateTime(model.DateValue),
                    CreateBy = loginUserId,
                    CreateDate = DateTime.Now,

                };
                await _connection.TokenMaster.AddRangeAsync(masterTb);
                await _connection.SaveChangesAsync();

                foreach (var item in model.Items)
                {
                    var master_details = new TokenDetails
                    {

                        TokenId = masterTb.TokenId,
                        CategoryId = Convert.ToInt32(item.CategoryId),
                        CategoryWiseId =Convert.ToInt32(item.Id),
                        Comments = item.Comment,
                        DataProperty=item.SelectValue,
                    };

                    await _connection.TokenDetails.AddRangeAsync(master_details);


                    if (item.Files != null && item.Files.Count > 0)
                    {
                        foreach (var item1 in item.Files)
                        {

                            if (!Directory.Exists(_imagePath))
                                Directory.CreateDirectory(_imagePath);

                            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(item1.FileName)}";
                            var savePath = Path.Combine(_imagePath, fileName);

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await item1.CopyToAsync(stream);
                            }

                            var master_details_image = new TokenDetailsImage
                            {

                                TokenId = masterTb.TokenId,
                                CategoryId = Convert.ToInt32(item.CategoryId),
                                CategoryWiseId =Convert.ToInt32(item.Id),
                                ImageName = fileName,
                                IsActive = 1,
                            };
                            await _connection.TokenDetailsImage.AddRangeAsync(master_details_image);

                        }

                    }


                }
                await _connection.SaveChangesAsync();
                await transaction.CommitAsync();

                return ("Token Save Successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ProductShowcaseDto>> getTokenDetailsShow(int TokenId)
        {
            try
            {

                
                var parameters = new[]
                    {
                            new NpgsqlParameter("p_token_id", TokenId),
                            
                    };

                var tokenDetailsList = await _connection.TokenDetailsShowDto
                    .FromSqlRaw("SELECT * FROM get_token_details(@p_token_id)", parameters)
                    .ToListAsync();


                var productShowcases = tokenDetailsList
                            .GroupBy(x => new { x.TokenId, x.CategoryWiseId }) // group by product
                            .Select(g => new ProductShowcaseDto
                            {
                                Id = $"product-{g.Key.TokenId}-{g.Key.CategoryWiseId}",
                                Category = g.First().CategoryName,
                                Title = g.First().Title,
                                Comments = g.First().Comments ?? string.Empty,
                                Images = g.Select(x => x.ImageName ).ToList()
                            })
                           .ToList();




                return productShowcases;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TokenMasterDto>> getTokenDataFilter(int? typeId, int? districtId, int? divisionId, int? ValueTypeId)
        {
            try
            {

                var parameters = new[]
                    {
                            new NpgsqlParameter("p_typeid", typeId== 0 ? 0 : typeId),
                            new NpgsqlParameter("p_distid", districtId== 0 ? 0 : districtId),
                            new NpgsqlParameter("p_divId", divisionId== 0 ? 0 : divisionId),
                            new NpgsqlParameter("p_valueId", ValueTypeId== 0 ? 0 : ValueTypeId),

                    };

                var tokenDetailsList = await _connection.TokenMasterDto
                    .FromSqlRaw("SELECT * FROM public.get_token_master_data_filter(@p_typeid,@p_distid,@p_divId,@p_valueId)", parameters)
                    .ToListAsync();

                return tokenDetailsList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
