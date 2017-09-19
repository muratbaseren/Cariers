using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class Social
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(30), DisplayName("Social")]
        public string IconName { get; set; }

        [Required, StringLength(255)]
        public string Url { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}