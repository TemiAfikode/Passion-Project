using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassionProject.Models
{
    public class BaseEntity
    {
        [Key]

        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
