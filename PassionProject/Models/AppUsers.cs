using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class AppUsers : IdentityUser
    {
        //[Key]
        //public Guid Id { get; set; }


        public DateTime? CreatedAt { get; set; }
    }

    public class AppUserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Emailaddress { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
