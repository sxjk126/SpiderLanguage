using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SpiderLanguage.Data;
using SpiderLanguage.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SpiderLanguage.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    [Authorize(Roles = "Administrator")]
    public class TeacherController : Controller
    {
        private SpiderLanguageContext _context;

        public TeacherController(SpiderLanguageContext spiderLanguageContext)
        {
            _context = spiderLanguageContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddChapter()
        {
            
            return View();
        }

        [HttpPost, ActionName("AddChapter")]
        [ValidateAntiForgeryToken]
        public IActionResult AddChapterPost(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                if (chapter.PhotoAvatar != null && chapter.PhotoAvatar.Length > 0)
                {
                    chapter.ImageMimeType = chapter.PhotoAvatar.ContentType;
                    chapter.ImageName = Path.GetFileName(chapter.PhotoAvatar.FileName);
                    using (var memoryStream = new MemoryStream())
                    {
                        chapter.PhotoAvatar.CopyTo(memoryStream);
                        chapter.PhotoFile = memoryStream.ToArray();
                    }
                    chapter.Available = true;
                    _context.Add(chapter);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(ThankYou));
            }
            return View();
        }

        public IActionResult ThankYou()
        {
            return View();
        }

    }
}