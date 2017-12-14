using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : Controller
    {
        public Student St = new Student(); 

        // GET: Students
        public ActionResult Index()
        {
            TempData["ASD"] = TempData["asd"];
            return View();
        }
        public ActionResult ViewBagExamples()
        {
            //ViewBag With Bulti-in DataType
            ViewBag.MyName = "ahmed";
            //ViewBag With One Object
            St.Id = 1;
            St.Name = "moh";
            St.Age = 20;
            ViewBag.StId = St.Id;
            ViewBag.StName = St.Name;
            ViewBag.StAge = St.Age;
            //ViewBag With List of Objects
            List<Student> Students = new List<Student>()
            {
               new Student(){Id=2,Name="SAFIA",Age=23},
               new Student(){Id=3,Name="nada",Age=20},
               new Student(){Id=4,Name="cariman",Age=25},
            };
            ViewBag.AllStudents = Students;
            return View();
        }


        public ActionResult ViewDataExamples()
        {
            //ViewData With Bulti-in DataType
            ViewData["Name"]= "ahmed";
            //ViewData With One Object
            St.Id = 1;
            St.Name = "moh";
            St.Age = 20;
            ViewData["StId"] = St.Id;
            ViewData["StName"] = St.Name;
            ViewData["StAge"] = St.Age;
            //ViewData With List of Objects
            List<Student> Students = new List<Student>()
            {
               new Student(){Id=2,Name="SAFIA",Age=23},
               new Student(){Id=3,Name="nada",Age=20},
               new Student(){Id=4,Name="cariman",Age=25},
            };
            ViewData["AllStudents"] = Students;
            return View();
        }

        public ActionResult TemDataExamples()
        {
            TempData["asd"] = "ASD";
            return new RedirectResult(@"~\Students\Index\");
          
        }

        public ActionResult ViewModelExamples()
        {
            
            List<Student> Students = new List<Student>()
            {
               new Student(){Id=2,Name="SAFIA",Age=23},
               new Student(){Id=3,Name="nada",Age=20},
               new Student(){Id=4,Name="cariman",Age=25},
            };
            return View(Students);
        }
    }
}