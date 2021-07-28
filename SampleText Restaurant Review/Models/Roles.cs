using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SampleText_Restaurant_Review.Models
{
    public class Roles : IdentityRole
    {
        public string Desc { get; set; }
        public DateTime DateCreated { get; set; }
        public string IPAddress { get; set; }
    }
}
