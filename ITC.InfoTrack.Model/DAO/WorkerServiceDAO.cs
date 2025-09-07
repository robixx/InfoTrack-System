
using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;


namespace ITC.InfoTrack.Model.DAO
{
    public class WorkerServiceDAO : IWorker
    {

        private readonly DatabaseConnection _connection;
        private readonly string _imagePath;
        public WorkerServiceDAO(DatabaseConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _imagePath = configuration["ImageStorage:TokenImagePath"];
        }

       

        public async Task<(string message, bool status)> SaveWorkerLogAsync(VisitLogInsertDto model, List<IFormFile> files, string LoginUserName)
        {
            if (model == null)
                return ("Invalid data", false);

            try
            {
                // Begin transaction
                await using var transaction = await _connection.Database.BeginTransactionAsync();

                // Get schedule
                var datetyime = await _connection.VisitSchedule
                    .FirstOrDefaultAsync(i => i.ScheduleId == model.ScheduleId);

                if (datetyime == null)
                    return ("Schedule not found", false);

                // Save visit log
                var visitdata = new VisitLog
                {
                    ScheduleId = model.ScheduleId,
                    Comments = model.Comments,
                    AssignedLog = datetyime.AssignUserId,
                    ResourceId = model.ResourceId,
                    CreateBy = model.CreateBy,
                    CreateDate = DateTime.Now,
                    AssignDateTime = datetyime.DateOfVisit,
                    CheckOutTime = DateTime.Now.TimeOfDay,
                    VisitTime = datetyime.TimeOfVisit,
                };

                await _connection.VisitLog.AddAsync(visitdata);
                await _connection.SaveChangesAsync();

                var fontFamily = SystemFonts.CreateFont("Arial", 13, FontStyle.Regular);  //SystemFonts.Families.First();

                foreach (var file in files)
                {
                    if (file.Length == 0) continue;

                    var safeName = Path.GetFileName(file.FileName);
                    var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{safeName}";
                    var filePath = Path.Combine(_imagePath, fileName);

                    using (var image = Image.Load<Rgba32>(file.OpenReadStream()))
                    {
                        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var text = $"{LoginUserName} | {timestamp}";
                        // Set proportional font size (1/10th of image width)
                        //float fontSize = 12f;
                        var font = SystemFonts.CreateFont("Arial", 12, FontStyle.Regular); //new Font(fontFamily, fontSize, FontStyle.Bold);
                        var textOptions = new TextOptions(font)
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom
                        };


                       
                        var textSize = TextMeasurer.MeasureSize(text, textOptions);
                        var position = new PointF(10, image.Height - textSize.Height - 10);

                        // Draw timestamp
                        image.Mutate(ctx =>
                        {
                            // Optional: add shadow for better visibility
                            ctx.DrawText(text, font, Color.Black, new PointF(position.X + 2, position.Y + 2));
                            ctx.DrawText(text, font, Color.Yellow, position);
                        });

                       
                        await image.SaveAsync(filePath);
                    }

                    
                    var imageRecord = new VisitLogDetails
                    {
                        VisitLogId = visitdata.VisitLogId,
                        AssignedLog = model.CreateBy,
                        ImageName = fileName,
                    };

                    _connection.VisitLogDetails.Add(imageRecord);
                  
                }

                var updateSchedule = await _connection.VisitSchedule.FirstOrDefaultAsync(i => i.ScheduleId == model.ScheduleId);
                if (updateSchedule != null)
                {
                    updateSchedule.IsVisited = 1;
                }

                await _connection.SaveChangesAsync();


                await transaction.CommitAsync();

                return ("Images and visit log saved successfully", true);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false);
            }
        }


        public async Task<List<DataMappingDto>> getDataMapAsync(int Id)
        {
            try
            {
                var parameters = new[]
                     {
                            new NpgsqlParameter("p_param", Id),

                    };

                var summarylist = await _connection.DataMappingDto
                    .FromSqlRaw("SELECT * FROM get_datamapping(@p_param)", parameters)
                    .ToListAsync();

                return summarylist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> SaveDataMapAsync(int divisionId, List<DataMappingDto> dto)
        {
            try
            {
                using var transaction = await _connection.Database.BeginTransactionAsync();

                if (dto==null || dto.Count==0) 
                {
                    var existing = await _connection.DataMapping
                          .Where(d=>d.DivisionId==divisionId).ToListAsync();
                    if (existing != null  || existing.Count!=0)
                    {
                        foreach (var d in existing)
                        {
                            d.DivisionId = 0;
                        }
                    }
                }
                else
                {
                    foreach (var item in dto)
                    {
                        // Option 1: Check if record exists for this division, type, source
                        var existing = await _connection.DataMapping
                            .FirstOrDefaultAsync(d => d.TypeId == item.TypeId &&
                                                      d.SourceId == item.SourceId);
                        if (existing != null)
                        {
                            existing.DivisionId = divisionId;
                            existing.CreatedAt = DateTime.Now;
                        }

                    }
                }                    

                await _connection.SaveChangesAsync();
                await transaction.CommitAsync();

                return ("Data Updated successfully", true);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<VisitLogGallarayDto>> GetGallaryDataAsync()
        {
            try
            {
                var visitLogs = await _connection.VisitLogGallarayDto
                           .FromSqlRaw("Select * from public.get_gallery_data()") // or use your SQL query
                           .ToListAsync();

               

                return visitLogs;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
