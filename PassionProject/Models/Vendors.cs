using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public partial class Vendors : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }

        public string VendorName { get; set; }


        public string Emailaddress { get; set; }

   
        public string VendorDescription { get; set; }
    }

    public class VendorDto
    {
        public int Id { get; set; }

        public string VendorName { get; set; }

        public string VendorDescription { get; set; }

        public string Emailaddress { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}