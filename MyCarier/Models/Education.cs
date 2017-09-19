using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50), DisplayName("School Name")]
        public string SchoolName { get; set; }

        [DisplayName("Start Year")]
        public int StartYear { get; set; }

        [DisplayName("End Year")]
        public int? EndYear { get; set; }

        [DisplayName("Is Current")]
        public bool IsCurrent { get; set; }
        public string Description { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}