using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Bookings :BaseEntity
    {
        //[Key]

        //public int Id { get; set; }

        //[ForeignKey("AppUsers")]
        public string UserId { get; set; }

        //public virtual AppUsers User { get; set; }

        [ForeignKey("Vendors")]
        public int VendorId { get; set; }

        public virtual Vendors Vendors { get; set; }



    }

    public class BookingDto
    {
        public int BookingId { get; set; }

        public int UserID { get; set; }

        public int VendorId { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DateDeleted { get; set; }


    }
}