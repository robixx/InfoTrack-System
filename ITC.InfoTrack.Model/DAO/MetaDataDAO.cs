using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class MetaDataDAO : IMetaData
    {
        private readonly DatabaseConnection _connection;
        public MetaDataDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(MetaElementInsertDto data, bool status)> EditMetaElement(int metaId)
        {
            try
            {
                var chekdata = await _connection.MetaDataType
                      .Where(i => i.PropertyId == metaId)
                      .FirstOrDefaultAsync();

                if (chekdata == null)
                    return (null, false); // Return status false if not found

                var result = new MetaElementInsertDto
                {
                    metaId = chekdata.PropertyId,
                    element = chekdata.PropertyName,
                    isActive = chekdata.PropertyStatus == 1,
                    description = chekdata.Description
                };

                return (result, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(MetaDataPropertyElementInsertDto data, bool status)> EditMetaPropertyElement(int dataElemetId)
        {
            try
            {
                var chekdata = await _connection.MetaDataElements
                      .Where(i => i.DataElementId == dataElemetId)
                      .FirstOrDefaultAsync();

                if (chekdata == null)
                    return (null, false); // Return status false if not found

                var result = new MetaDataPropertyElementInsertDto
                {
                    dataElementId = chekdata.DataElementId,
                    groupId = chekdata.PropertyId,
                    isActive = chekdata.DataElementStatus == 1,
                    parentValueId = chekdata.ParentPropertyId ?? 0,
                    propertyName = chekdata.MetaElementValue,
                    orderview = chekdata.PropertyViewOrder
                };

                return (result, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MetaElementInsertDto>> getMetaElement()
        {
            try
            {
                var chekdata = await _connection.MetaDataType.ToListAsync();
                var result = chekdata.Select(x => new MetaElementInsertDto
                {
                    metaId = x.PropertyId,
                    element = x.PropertyName,
                    isActive = x.PropertyStatus == 1 ? true : false,
                    description = x.Description
                }).ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MetaDataPropertyElementGetDto>> getMetaPropertyElement()
        {
            try
            {
                var data = await (
                        from Mp in _connection.MetaDataElements
                        join Me in _connection.MetaDataType on Mp.PropertyId equals Me.PropertyId

                        // Self-join to get parent MetaElementValue
                        join parentElement in _connection.MetaDataElements
                            on Mp.ParentPropertyId equals parentElement.DataElementId into parentJoin
                        from parent in parentJoin.DefaultIfEmpty() // LEFT JOIN for root elements

                        select new MetaDataPropertyElementGetDto
                        {
                            DataElementId = Mp.DataElementId,
                            MetaElement = Me.PropertyName,
                            PropertyName = Mp.MetaElementValue,
                            ParentData = Mp.ParentPropertyId == 0 ? "Root" : parent.MetaElementValue,
                            ViewOrder = Mp.PropertyViewOrder,
                            isActive = Mp.DataElementStatus == 1
                        }
                    ).OrderBy(i => i.ViewOrder)
                     .AsNoTracking()
                     .ToListAsync();

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        public async Task<(string message, bool status)> SaveMetaElement(MetaElementInsertDto model)
        {
            try
            {
                string message = "";
                bool status = false;
                if (model == null)
                {
                    return ("MetaData Insert Faild", false);

                }
                if (model.metaId > 0)
                {
                    var chekdata = await _connection.MetaDataType
                        .Where(i => i.PropertyId == model.metaId)
                        .FirstOrDefaultAsync();
                    if (chekdata != null)
                    {
                        chekdata.PropertyName = model.element;
                        chekdata.Description = model.description;
                        chekdata.PropertyStatus = model.isActive ? 1 : 0;
                        int result = await _connection.SaveChangesAsync();
                    }
                    return ("MetaData Update Successfully", true);

                }
                else
                {
                    var avoidDuplicate = await _connection.MetaDataType
                        .Where(i => i.PropertyName == model.element.Trim())
                        .FirstOrDefaultAsync();
                    if (avoidDuplicate != null)
                    {
                        return ("Already Property Name has been taken.", false);
                    }
                    var meta = new MetaDataType
                    {
                        PropertyName = model.element,
                        Description = model.description,
                        PropertyStatus = model.isActive == false ? 0 : 1,
                    };
                    await _connection.MetaDataType.AddRangeAsync(meta);
                    await _connection.SaveChangesAsync();
                    message = "MetaData Save Successfully";
                    status = true;
                }

                return (message, status);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false);
            }

        }

        public async Task<(string message, bool status)> SaveMetaElementProperty(MetaDataPropertyElementInsertDto model)
        {
            try
            {
                string message = "";
                bool status = false;
                if (model == null)
                {
                    return ("MetaData Insert Faild", false);

                }
                if (model.dataElementId > 0)
                {
                    var chekdata = await _connection.MetaDataElements
                        .Where(i => i.DataElementId == model.dataElementId)
                        .FirstOrDefaultAsync();
                    if (chekdata != null)
                    {
                        chekdata.PropertyId = model.groupId;
                        chekdata.ParentPropertyId = model.parentValueId;
                        chekdata.MetaElementValue = model.propertyName;
                        chekdata.PropertyViewOrder = model.orderview;
                        chekdata.DataElementStatus = model.isActive ? 1 : 0;
                        int result = await _connection.SaveChangesAsync();
                    }
                    return ("MetaData Update Successfully", true);

                }
                else
                {
                    var avoidDuplicate = await _connection.MetaDataElements
                        .Where(i => i.MetaElementValue == model.propertyName.Trim() && i.ParentPropertyId == model.parentValueId && i.PropertyId == model.groupId)
                        .FirstOrDefaultAsync();
                    if (avoidDuplicate != null)
                    {
                        return ("Already Property Name has been taken.", false);
                    }
                    var meta = new MetaDataElements
                    {

                        PropertyId = model.groupId,
                        ParentPropertyId = model.parentValueId,
                        MetaElementValue = model.propertyName,
                        PropertyViewOrder = model.orderview,
                        DataElementStatus = model.isActive ? 1 : 0,

                    };
                    await _connection.MetaDataElements.AddRangeAsync(meta);
                    await _connection.SaveChangesAsync();
                    message = "MetaData Property Save Successfully";
                    status = true;

                }

                return (message, status);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
