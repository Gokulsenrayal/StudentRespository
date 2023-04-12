using LinqStoringData.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LinqStoringData.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext() : base("name=StudentDbContext")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

       
    }
}