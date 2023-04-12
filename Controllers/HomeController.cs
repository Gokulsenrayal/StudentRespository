using LinqStoringData.Data;
using LinqStoringData.Models;
using LinqStoringData.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace LinqStoringData.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDbContext db = new StudentDbContext();

        public ActionResult ListOfRecord()
        {
            try
            {
                var record = (from std in db.Students
                              join sub in db.Subjects
                              on std.Id equals sub.StudentId
                              select new TotalCalculation
                              {
                                  Name = std.Name,
                                  SubjectName = sub.SubjectName,
                                  Marks = sub.Marks
                              }).OrderBy(x => x.Name).ThenBy(s => s.SubjectName).ToList();

                var records = db.Students.Join(db.Subjects,
                                 std => std.Id,
                                 sub => sub.StudentId, (std, sub) => new TotalCalculation
                                 {
                                     Name = std.Name,
                                     SubjectName = sub.SubjectName,
                                     Marks = sub.Marks

                                 }).OrderBy(x => x.Name).ThenBy(s => s.SubjectName).ToList();
                return View(records);
            }
            catch(Exception )
            {
                ViewBag.Message = "Error";
                return View();
            }
         }
      
    public ActionResult TotalMarks()
        {
            //var total = (from std in db.Students
            //             join sub in db.Subjects on std.Id equals sub.StudentId
            //             group sub by std.Id into g
            //             select new TotalCalculation
            //             {
            //                 Name = g.Select(x => x.Student.Name).FirstOrDefault(),
            //                 SubjectName = g.Select(s => s.SubjectName).FirstOrDefault(),
            //                 TotalMarks = g.Where(x => x.SubjectName.Contains(x.SubjectName)).Max(x => x.Marks),
            //             }).OrderByDescending(s => s.TotalMarks).ToList();
            try
            {
                var total = (from std in db.Students
                             join sub in db.Subjects
                             on std.Id equals sub.StudentId
                             group sub by sub.Student.Id into g
                             select new TotalCalculation
                             {
                                 Name = g.Select(x => x.Student.Name).FirstOrDefault(),
                                 BoilogyMarks = g.Where(s => s.SubjectName.Contains("Biology")).Sum(x => x.Marks),
                                 ChemistryMarks = g.Where(s => s.SubjectName.Contains("Chemistry")).Sum(x => x.Marks),
                                 MathsMarks = g.Where(s => s.SubjectName.Contains("Maths")).Sum(x => x.Marks),
                                 TotalMarks = g.Where(x => x.Id == x.Id).Sum(x => x.Marks)
                             }).ToList();
                return View(total);
            }
            catch (Exception)
            {
                ViewBag.Message = "Error";
                return View();
            }
        }

    
        public ActionResult Filter()
        {
            try 
            { 
            var percentage = (from std in db.Students
                              join sub in db.Subjects on std.Id equals sub.StudentId
                              group sub by new {std.Id} into g
                              select new TotalCalculation
                              {
                                  Name =g.Select(x=>x.Student.Name).FirstOrDefault(),
                                  Percentage = g.Sum(s => s.Marks) * 100 / 400
                              }).ToList();

            var sorting = (from percent in percentage
                           group percent by new { percent.Name } into g
                           select new TotalCalculation
                           {
                               Name = g.Key.Name,
                               Highest = g.Select(x => x.Percentage).Where(x => x > 80).FirstOrDefault(),
                               Intermediate = g.Select(x => x.Percentage).Where(x => x > 60 && x < 80).FirstOrDefault(),
                               Lowest = g.Select(x => x.Percentage).Where(x => x < 60).FirstOrDefault(),
                           }).ToList();
            return View(sorting);
        }
            catch (Exception)
            {
                ViewBag.Message = "Error";
                return View();
            }
        }

        //public ActionResult HighScorer()
        //{
        //    var score = (from std in db.Students
        //                 join sub in db.Subjects on std.Id equals sub.StudentId

        //                 group sub by new { sub.SubjectName } into g


        //                 select new TotalCalculation
        //                 {
        //                     SubjectName = g.Select(x=>x.SubjectName).FirstOrDefault(),
        //                     Marks = g.Max(x => x.Marks),
        //                     Name = g.Where(m => m.Marks == g.Max(x => x.Marks)).Select(x => x.Student.Name).FirstOrDefault()
        //                 }).ToList();


        //    return View(score); 
        //}

        //[HttpGet]
        //public ActionResult HighScorer()
        //{
        //    var score = (from sub in db.Subjects
        //                           group sub by sub.StudentId into g
        //                           select new TotalCalculation
        //                           {
        //                               SubjectName = g.Max(x=>x.SubjectName),
        //                               Marks = g.Max(m => m.Marks),
        //                               Name = g.Where(m => m.Marks == g.Max(s => s.Marks))
        //                           .Select(n => n.Student.Name).FirstOrDefault(),
        //                           }).ToList();
        //    return View(score);
        //}
        public ActionResult HighScorer()
        {
            //    var score = (from std in db.Students
            //                 join sub in db.Subjects on std.Id equals sub.StudentId
            //                 group sub by new { sub.StudentId } into grp

            //                 select new TotalCalculation
            //                 {
            //                     SubjectName = grp.Max(m => m.SubjectName),
            //                     Marks = grp.Max(m => m.Marks),
            //                     Name = grp.Where(m => m.Marks == grp.Max(x => x.Marks))
            //                     .Select(x => x.Student.Name).FirstOrDefault(),
            //                 }).ToList();
            //    return View(score);
            //}
            try
            {

                var highestScores = db.Subjects
                 .GroupBy(s => s.SubjectName, (key, marks) => new 
                 {
                     SubjectName = key,
                     Marks = marks.Max(s => s.Marks),
                     Students = marks.Where(s => s.Marks == marks.Max(x => x.Marks)).Select(x => x.Student.Name).ToList(),
                 }).OrderByDescending(x=>x.Marks).ToList();
                
                List<TotalCalculation> list = new List<TotalCalculation>();
                foreach (var item in highestScores)
                {
                    foreach(var student in item.Students)
                    {
                        list.Add(new TotalCalculation
                        {
                           Name=student,
                           SubjectName=item.SubjectName,
                           Marks=item.Marks
                        });
                    }
                 }
                return View(list);
            }

            catch (Exception)
            {
                ViewBag.Message = "Error";
                return View();
            }
        }


        public ActionResult LowScorer()
        {
            try
            {
                var lowestScores = db.Subjects
                 .GroupBy(s => s.SubjectName, (key, marks) => new TotalCalculation
                 {
                     SubjectName = key,
                     Marks = marks.Min(s => s.Marks),
                     Name = marks.Where(s => s.Marks == marks.Min(x => x.Marks)).Select(x => x.Student.Name).FirstOrDefault(),
                 }).ToList();
                return View(lowestScores);  
                
        }
            catch (Exception)
            {
                ViewBag.Message = "Error";
                return View();
            }
        }
    }
}

//var highestScores = db.Subjects
//                     .GroupBy(s => s.StudentId, (key, grp) => new TotalCalculation
//                     {
//                         SubjectName = grp.Where(s => s.Marks == grp.Max(x => x.Marks) && s.Marks != 0).Select(x => x.SubjectName).FirstOrDefault(),
//                         Marks = grp.Max(s => s.Marks),
//                         Name = grp.Where(s => s.Marks == grp.Max(x => x.Marks) && s.Id == s.Id).Select(x => x.Student.Name).FirstOrDefault(),
//                     }).ToList();
//return View(highestScores);