using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class WorkExprience
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(40)]
        public string Title { get; set; }

        [Required, StringLength(50), DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Start Date"), 
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date"), 
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayName("Is Current")]
        public bool IsCurrent { get; set; }
        public string Description { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}