using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SpiderLanguage.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentId { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
        
    }
}
