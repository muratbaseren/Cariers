using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class PersonInfo
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required, StringLength(25)]
        public string Surname { get; set; }

        [StringLength(60)]
        public string Title { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        [StringLength(25)]
        public string Country { get; set; }

        [Required, StringLength(60)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(50), DisplayName("Profile Photo")]
        public string PhotoImageName { get; set; }

        [StringLength(350), DisplayName("Addtional Info")]
        public string AddtionalInfo { get; set; }
    }
}