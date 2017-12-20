using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uploding_Display_Files.Controllers
{
    public class Uploding_DisplayController : Controller
    {
        /*#############################################
        *              UplodingAnyFiles
        * ############################################*/
        [HttpGet]
        public ActionResult UplodingAnyFiles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UplodingAnyFiles(HttpPostedFileBase MyFile,string FileName)
        {
            if( MyFile.ContentLength > 0 )
            {
                //original File Name
                string originalFileName = Path.GetFileNameWithoutExtension(MyFile.FileName);
                string Extention = Path.GetExtension(MyFile.FileName);
                //path of the folder which contain the file
                string FolderPath = "~/Files/AnyFile";
                //make new name because user can uolode the same file again
                string NewFileName = FileName +  DateTime.Now.ToString("yyyyMMddHHmmss") + Extention;
                //combine path of my Folder in server and my File name
                string CompletePath =  Path.Combine(Server.MapPath(FolderPath), NewFileName);
                //save the uploding file
                MyFile.SaveAs(CompletePath);
                //create successful message for the user
                ViewBag.Message = "File Uploaded Successfully !!"; 
            }
            else
            {
                ViewBag.Message = "File Uploaded Failed !! ";
            }

            return View();
        }

        /*#############################################
       *              UplodingImageOnly
       * ############################################*/
        [HttpGet]
        public ActionResult UplodingImageOnly()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UplodingImageOnly(HttpPostedFileBase MyFile, string FileName)
        {
            if (MyFile.ContentLength > 0)
            {
                //original File Name
                string originalFileName = Path.GetFileNameWithoutExtension(MyFile.FileName);
                string Extention = Path.GetExtension(MyFile.FileName);
                //check the file is image or not 
                switch(Extention.ToLower()) 
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        //path of the folder which contain the file
                        string FolderPath = "~/Files/Image";
                        string FolderPathForHtml = "../Files/Image/";
                        //make new name because user can uolode the same file again
                        string NewFileName = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + Extention;
                        //combine path of my Folder in server and my File name
                        string CompletePath = Path.Combine(Server.MapPath(FolderPath), NewFileName);
                        //save the uploding file
                        MyFile.SaveAs(CompletePath);
                        //create successful message for the user
                        ViewBag.Message = "IMAGE Uploaded Successfully !!";
                        ViewBag.ImageSrc = FolderPathForHtml + NewFileName;
                        break;
                    default:
                        ViewBag.Message = "THE IMAGE MUST BE .png or jpg or .jpeg ";
                        return View(); 
                }   
            }
            else
            {
                ViewBag.Message = "File Uploaded Failed !! ";
            }

            return View();
        }

        /*#############################################
         *              UplodingVideoOnly
         * ############################################*/
        [HttpGet]
        public ActionResult UplodingVideoOnly()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UplodingVideoOnly(HttpPostedFileBase MyFile, string FileName)
        {
            if (MyFile.ContentLength > 0)
            {
                //original File Name
                string originalFileName = Path.GetFileNameWithoutExtension(MyFile.FileName);
                string Extention = Path.GetExtension(MyFile.FileName);
                switch(Extention.ToLower())
                {
                    case ".mp4":
                    case ".flv":
                    case ".avi":
                    case ".wmv":
                    case ".mov":
                        //path of the folder which contain the file
                        string FolderPath = "~/Files/Video";
                        string FolderPathForHtml = "../Files/Video/";
                        //make new name because user can uolode the same file again
                        string NewFileName = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + Extention;
                        //combine path of my Folder in server and my File name
                        string CompletePath = Path.Combine(Server.MapPath(FolderPath), NewFileName);
                        //save the uploding file
                        MyFile.SaveAs(CompletePath);
                        //create successful message for the user
                        ViewBag.Message = "Video Uploaded Successfully !!";
                        ViewBag.ImageSrc = FolderPathForHtml + NewFileName;
                        break;
                    default:
                        ViewBag.Message = "THE video MUST BE .mp4 or .flv or .avi or .wmv or .mov ";
                        return View();
                }      
            }
            else
            {
                ViewBag.Message = "File Uploaded Failed !! ";
            }

            return View();
        }
        /*#############################################
        *              UplodingPDFOnly
        * ############################################*/
        [HttpGet]
        public ActionResult UplodingPDFOnly()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UplodingPDFOnly(HttpPostedFileBase MyFile, string FileName)
        {
            string CompletePath = " ";
            if (MyFile.ContentLength > 0)
            {
                //original File Name
                string originalFileName = Path.GetFileNameWithoutExtension(MyFile.FileName);
                string Extention = Path.GetExtension(MyFile.FileName);
                //Complete Path un the server
                switch (Extention.ToLower())
                {
                    case ".pdf":
                        //path of the folder which contain the file
                        string FolderPath = "~/Files/Video";
                        //make new name because user can uolode the same file again
                        string NewFileName = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + Extention;
                        //combine path of my Folder in server and my File name
                        CompletePath = Path.Combine(Server.MapPath(FolderPath), NewFileName);
                        //save the uploding file
                        MyFile.SaveAs(CompletePath);
                        //create successful message for the user
                        ViewBag.Message = "file Uploaded Successfully !!";
                        
                        break;
                    default:
                        ViewBag.Message = "THE file MUST BE .pdf ";
                        return View();
                }
            }
            else
            {
                ViewBag.Message = "File Uploaded Failed !! ";
                return View();
            }
            TempData["FullPath"] = CompletePath;
            return RedirectToAction("DisplayPDF");
        }

        public FileResult DisplayPDF()
        {
           
            byte[] pdfByte = GetBytesFromFile(TempData["FullPath"].ToString());
            return File(pdfByte, "application/pdf");
        }

        [NonAction]
        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /*#############################################
      *              UplodingPDFOnly
      * ############################################*/
        [HttpGet]
        public ActionResult UplodingPDFOnly2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UplodingPDFOnly2(HttpPostedFileBase MyFile, string FileName)
        {
            string CompletePath = " ";
            if (MyFile.ContentLength > 0)
            {
                //original File Name
                string originalFileName = Path.GetFileNameWithoutExtension(MyFile.FileName);
                string Extention = Path.GetExtension(MyFile.FileName);
                //Complete Path un the server
                switch (Extention.ToLower())
                {
                    case ".pdf":
                        //path of the folder which contain the file
                        string FolderPath = "~/Files/Video";
                        //make new name because user can uolode the same file again
                        string NewFileName = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + Extention;
                        //combine path of my Folder in server and my File name
                        CompletePath = Path.Combine(Server.MapPath(FolderPath), NewFileName);
                        //save the uploding file
                        MyFile.SaveAs(CompletePath);
                        //create successful message for the user
                        ViewBag.Message = "file Uploaded Successfully !!";

                        break;
                    default:
                        ViewBag.Message = "THE file MUST BE .pdf ";
                        return View();
                }
            }
            else
            {
                ViewBag.Message = "File Uploaded Failed !! ";
                return View();
            }
            TempData["FullPath"] = CompletePath;
            return RedirectToAction("DisplayPDF2");
        }
        [NonAction]
        public FileResult DisplayPDF2()
        {
            string MyFile = TempData["FullPath"].ToString();
            Response.AppendHeader("Content-Disposition", "inline; filename=" + MyFile );
            return File(MyFile, "application/pdf");
            
        }



    }
}