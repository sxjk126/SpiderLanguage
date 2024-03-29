﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpiderLanguage.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Date Published")]
        public string DatePublished { get; set; }
        public bool Available { get; set; }
        public string ImageName { get; set; }
        public byte[] PhotoFile { get; set; }
        public string ImageMimeType { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile PhotoAvatar { get; set; }
        public bool Recommended { get; set; }

        //[InverseProperty("Books")]
        public virtual User User { get; set; }
    }
}
