using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Services : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }

        public string ServiceName { get; set; }

        [ForeignKey("Vendors")]
        public int VendorId { get; set; }

        public virtual Vendors Vendors { get; set; }

        public Double Fee { get; set; }

   
    }

    public class ServiceDto
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public int VendorId { get; set; }

        public Double Fee { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}