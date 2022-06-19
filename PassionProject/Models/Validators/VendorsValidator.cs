using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class VendorsValidator 
    {
       
        [Required]
        [MaxLength(50)]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        [Required]
        [Display(Name = "Vendor Email")]
        public string Emailaddress { get; set; }

        [Required]
        [Display(Name = "Vendor Description")]
        public string VendorDescription { get; set; }
    }

    [ModelMetadataType(typeof(VendorsValidator))]
    public partial class Vendors
    {
    }
}