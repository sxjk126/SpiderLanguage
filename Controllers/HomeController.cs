using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpiderLanguage.Models;
using System.IO;
using SpiderLanguage.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace SpiderLanguage.Controllers
{
    public class HomeController : Controller
    {
        private SpiderLanguageContext _context;
        private IWebHostEnvironment _environment;

        public HomeController(SpiderLanguageContext spiderLanguageContext, IWebHostEnvironment environment)
        {
            _context = spiderLanguageContext;
            _environment = environment;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var chaptersQuery = from b in _context.Chapters
                             where b.Recommended == true
                             select b;

            return View(chaptersQuery);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetImage(int id)
        {
            Chapter requestedChapter = _context.Chapters.FirstOrDefault(b => b.Id == id);
            if (requestedChapter != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requestedChapter.ImageName;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedChapter.ImageMimeType);
                }
                else
                {
                    if (requestedChapter.PhotoFile.Length > 0)
                    {
                        return File(requestedChapter.PhotoFile, requestedChapter.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



  
    }
}
