using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class Login
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(60), DisplayName("E-mail Address")]
        public string Email { get; set; }

        [Required, StringLength(150)]
        public string Password { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}