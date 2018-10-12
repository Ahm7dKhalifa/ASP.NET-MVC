using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace CWR.Controllers
{
    public class CWRController : Controller
    {
        /*========================================
         * create file
         * =====================================*/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTextFile(string FileNameWithExtension, string Content)
        {
            string FullPath = Server.MapPath("~/Files/" + FileNameWithExtension);
            if(System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
            }
            System.IO.File.WriteAllText(FullPath, Content);
            return View();
        }
        /*=========================================
         * read file content
         * ========================================*/
        public ActionResult ReadFileContent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReadFileContent(string FileNameWithExtension)
        {
            string FullPath = Server.MapPath("~/Files/" + FileNameWithExtension);
            if (!System.IO.File.Exists(FullPath))
            {
                ViewBag.ErrorMessage = "File Not Exist";
                return View();
            }
            string FileContent =  System.IO.File.ReadAllText(FullPath);
            if(FileContent == null)
            {
                    ViewBag.ErrorMessage = "file is empty";
                    return View();
            }
            ViewBag.FileContent = FileContent;
            return View();
        }
    }
}