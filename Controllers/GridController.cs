using LinqStoringData.Data;
using System;
using System.Linq;
using System.Web.Mvc;
using LinqStoringData.ViewModel;

namespace LinqStoringData.Controllers
{
    public class GridController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

      
        public ActionResult GridFactor(string searchName)
           
        {
            try
            {
                var search = (from std in db.Students
                          join sub in db.Subjects
                          on std.Id equals sub.StudentId
                          select new TotalCalculation
                          {
                              Name = std.Name,
                              SubjectName = sub.SubjectName,
                              Marks = sub.Marks,
                          });
            if (!String.IsNullOrEmpty(searchName))
            {
                search = search.Where(p => p.Name.Contains(searchName) || p.SubjectName.Contains(searchName));
            }
            return View(search.ToList());
        }
    
            catch(Exception){
                ViewBag.Message = "Error";
                return View();
            }
    }
}
}