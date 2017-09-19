using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class SharedUrl
    {
        [Key]
        public Guid Id { get; set; }

        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required, ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}