using LinqStoringData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinqStoringData.ViewModel
{
    public class SubjectVm
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string SubjectName { get; set; }
        public int Marks { get; set; }
    }
}