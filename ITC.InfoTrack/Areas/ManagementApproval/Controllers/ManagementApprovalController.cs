using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITC.InfoTrack.Areas.ManagementApproval.Controllers
{
    [Area("ManagementApproval")]
    [Authorize]
    public class ManagementApprovalController : Controller
    {

        private readonly IWorker _worker;
        private readonly string _imagePath;
        public ManagementApprovalController(IWorker worker, IConfiguration configuration)
        {
            _worker = worker;
            _imagePath = configuration["ImageStorage:TokenImagePath"];
        }

        [HttpGet]
        public IActionResult LogApproval()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryData()
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);

            int roleid = Convert.ToInt32(User.FindFirst("RoleId").Value);

            var visitLogs = await _worker.GetGallaryDataAsync(userId, roleid);
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
                                    Comments=img.Comments
                                }).ToList()
                        )
                }).ToList();

            return Ok(result);
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
                _ => "application/octet-stream"
            };

            Response.Headers["Cache-Control"] = "public,max-age=3600"; // Cache 1 hour
            return PhysicalFile(path, mimeType);
        }


        
    }
}
