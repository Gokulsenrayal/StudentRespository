using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LinqStoringData.ViewModel
{
    public class TotalCalculation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Grade Total")]
        public int TotalMarks { get; set; }
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        public int Marks { get; set; }
        public int Percentage { get; set; }
        [Display(Name="Percentage above 80%")]
        public int Highest { get; set; }
        [Display(Name = "Percentage Below 60%")]
        public int Lowest { get; set; }
        [Display(Name = "Percentage Between 80% to 60%")]
        public int Intermediate { get; set; }
        public List<string> Students { get; set; }
        public List<string> Mark { get; set; }
        [Display(Name = "Biology Marks")]
        public int BoilogyMarks { get; set; }
        [Display(Name = "Chemistry Marks")]
        public int ChemistryMarks { get; set; }
        [Display(Name = "Maths Marks")]
        public int MathsMarks { get; set; }

    } 
}