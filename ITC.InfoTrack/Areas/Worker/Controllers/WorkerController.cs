using ClosedXML.Excel;
using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel;

namespace ITC.InfoTrack.Areas.Worker.Controllers
{
    [Area("Worker")]
    [Authorize]
    public class WorkerController : Controller
    {
        
        private readonly IDropDown _dropdown;
        private readonly ICorporate _corporate;
        private readonly IWorker _worker;
        public WorkerController(IDropDown dropDown, ICorporate corporate,IWorker worker)
        {
            _corporate = corporate;
            _dropdown = dropDown;
            _worker = worker;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int scheduleId)
        {
            var banks = new List<object>
                {
                    new { BankId = 1, BankName = "City Bank Limited" },                    
                };

            var selected_data= await _corporate.getSheduledata(scheduleId);


            ViewBag.Banks = new SelectList(banks, "BankId", "BankName");
           // ViewBag.type = new SelectList(await _dropdown.GetTokenType(), "Id", "Name");
            ViewBag.district = new SelectList(await _dropdown.getDistrict(), "Id", "Name",selected_data.DistrictId);
            ViewBag.division = new SelectList(await _dropdown.getDivision(), "Id", "Name",selected_data.DivisionId);
            ViewBag.source = new SelectList(await _dropdown.getTypeOFelement(selected_data.TypeId, selected_data.ValueTypeId), "Id", "Name"); 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVisitLog(int scheduleId, [FromForm] IFormCollection form, List<IFormFile> files)
        {

            int userId= Convert.ToInt32(User.FindFirst("UserId").Value);

            string loginuser= User.Identity?.Name;

            var bankId = form["BankId"].ToString();
            var districtId = form["DristictId"].ToString();
            var divisionId = form["DivisionId"].ToString();
            var sourceId = form["SourceId"].ToString();
            var comments = form["comments"].ToString();
            var schedId = form["scheduleId"].ToString();

            var visitLog = new VisitLogInsertDto
            {
                ScheduleId =Convert.ToInt32(schedId),
                ResourceId = 0,
                CreateBy = userId,
                DistrictId=Convert.ToInt32(districtId),
                DivisionId=Convert.ToInt32(divisionId),
                SourceId=Convert.ToInt32(sourceId),
                Comments = comments,
                
            };

            var result = await _worker.SaveWorkerLogAsync(visitLog, files, loginuser);

            return Json(new { message = result.message, status = result.status, redirectUrl = Url.Action("VisitLog", "Corporate", new { area = "Corporate" }) });

            
        }


        [HttpGet]
        public IActionResult ExeclInsert()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest(new { message = "No file uploaded" });

        //    try
        //    {
        //        var branches = new List<BranchInfo>();

        //        using (var stream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(stream); // 🔁 Use async copy
        //            stream.Position = 0; // 🔁 Reset position before reading

        //            using (var workbook = new XLWorkbook(stream))
        //            {
        //                var worksheet = workbook.Worksheets.FirstOrDefault();
        //                if (worksheet == null)
        //                    return BadRequest(new { message = "No worksheet found in Excel file." });

        //                var rows = worksheet.RangeUsed()?.RowsUsed();
        //                if (rows == null)
        //                    return BadRequest(new { message = "Excel file is empty." });

        //                foreach (var row in rows) // skip header
        //                {
        //                    try
        //                    {
        //                        var branch = new BranchInfo
        //                        {
        //                            BranchCode = int.TryParse(row.Cell(1).GetString().Trim(), out var bc) ? bc : 0,
        //                            BBCode = int.TryParse(row.Cell(2).GetString().Trim(), out var bb) ? bb : 0,
        //                            BranchName = row.Cell(3).GetString().Trim(),
        //                            AgriBranches = row.Cell(4).GetString().Trim(),
        //                            District = row.Cell(5).GetString().Trim(),
        //                            Division = row.Cell(6).GetString().Trim(),
        //                            Address = row.Cell(7).GetString().Trim()
        //                        };

        //                        branches.Add(branch);
        //                    }
        //                    catch (Exception innerEx)
        //                    {
        //                        return BadRequest(new { message = $"Error processing row: {innerEx.Message}" });
        //                    }
        //                }

        //                _connection.BranchInfo.AddRange(branches);
        //                await _connection.SaveChangesAsync();
        //            }
        //        }

        //        return Ok(new { message = "Excel file uploaded and data saved successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error: " + ex.Message });
        //    }
        //}
    }
    
}
