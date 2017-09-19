using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCarier.ViewModels
{
    public class PdfViewModel
    {
        public string Param { get; set; }
        public PersonInfo PersonInfo { get; set; }
        public List<Ability> Skills { get; set; }
        public List<Ability> Languages { get; set; }
        public List<WorkExprience> WorkExperiences { get; set; }
        public List<Education> Educations { get; set; }
    }
}