using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCarier.Models
{
    public class Ability
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(40)]
        public string Name { get; set; }

        [Required, Range(0,100),DisplayName("Level")]
        public int Scale { get; set; }

        [Required, StringLength(15)]
        public string Category { get; set; }

        public virtual PersonInfo PersonInfo { get; set; }
    }
}