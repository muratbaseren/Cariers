using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required, StringLength(25)]
        public string Surname { get; set; }

        [Required, StringLength(60), EmailAddress()]
        public string Email { get; set; }

        [Required, StringLength(12)]
        public string Password { get; set; }

        [Required, StringLength(12), Compare("Password")]
        public string RePassword { get; set; }
    }
}