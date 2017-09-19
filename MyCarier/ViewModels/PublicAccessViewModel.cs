using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.ViewModels
{
    public class PublicAccessViewModel
    {
        [Required, DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}