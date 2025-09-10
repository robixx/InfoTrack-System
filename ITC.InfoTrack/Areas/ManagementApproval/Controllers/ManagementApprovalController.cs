using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

namespace ITC.InfoTrack.Areas.ManagementApproval.Controllers
{
    [Area("ManagementApproval")]
    [Authorize]
    public class ManagementApprovalController : Controller
    {

        private readonly IWorker _worker;
        private readonly IDropDown _dropdown;
        private readonly string _imagePath;
        public ManagementApprovalController(IWorker worker, IConfiguration configuration, IDropDown dropdown)
        {
            _worker = worker;

            _imagePath = configuration["ImageStorage:TokenImagePath"];
            _dropdown = dropdown;
        }

        [HttpGet]
        public async Task<IActionResult> LogApproval()
        {
            ViewBag.area = new SelectList(await _dropdown.getArea(), "Id", "Name");
            ViewBag.type = new SelectList(await _dropdown.GetTokenTypeRequest(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryData(string typeId, string areaId, string divisionId, string valueTypeId)
        {
            
            int id = Convert.ToInt32(typeId);
            int area = Convert.ToInt32(areaId);
            int division = Convert.ToInt32(divisionId);
            int valueid = Convert.ToInt32(valueTypeId);


            var visitLogs = await _worker.GetGallaryDataAsync(id, area, division, valueid);
            // Group by DivisionName
            var result = visitLogs
                .GroupBy(v => v.DivisionName)
                .Select(divGroup => new DivisionDto
                {
                    DivisionName = divGroup.Key,
                    Branches = divGroup
                        .GroupBy(b => b.SourceName) // SourceName = BranchName
                        .ToDictionary(
                            branchGroup => branchGroup.Key, // BranchName
                            branchGroup => branchGroup
                                .OrderByDescending(img => img.CreateDate)
                                .Select(img => new ImageDto
                                {
                                    Src = Url.Action("GetImage", "ManagementApproval", new { fileName = img.ImageName }, Request.Scheme),           // your image path/url
                                    Alt = $"Image for {img.SourceName}",
                                    Branch = img.SourceName,       // BranchName from SourceName
                                    UploadDate = img.CreateDate,
                                    Comments=img.Comments,
                                    FileType = GetFileType(img.ImageName)
                                }).ToList()
                        )
                }).ToList();

            return Ok(result);
        }


        private string GetFileType(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLower();
            var imageExts = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var videoExts = new[] { ".mp4", ".avi", ".mov", ".mkv" };

            if (imageExts.Contains(ext)) return "image";
            if (videoExts.Contains(ext)) return "video";
            return "other";
        }

        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("File name is required.");

            var path = Path.Combine(_imagePath, fileName);

            if (!System.IO.File.Exists(path))
                return NotFound("/images/placeholder.png");

            var mimeType = Path.GetExtension(fileName).ToLower() switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                ".mkv" => "video/x-matroska",
                _ => "application/octet-stream"
            };

            Response.Headers["Cache-Control"] = "public,max-age=3600"; // Cache 1 hour
            return PhysicalFile(path, mimeType);
        }


        
    }
}
