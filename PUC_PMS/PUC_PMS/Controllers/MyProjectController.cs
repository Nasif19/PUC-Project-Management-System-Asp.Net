using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUC_PMS.Models;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Managers;
using System.Web.Helpers;
using System.IO;
namespace PUC_PMS.Controllers
{
    public class MyProjectController : Controller
    {
        private ProjectListManager ProjectListManager;
        private StudentProjectManager StudentProjectManager;
        private MyProjectManager MyProjectManager;
        public MyProjectController()
        {
            ProjectListManager = new ProjectListManager();
            StudentProjectManager = new StudentProjectManager();
            MyProjectManager = new MyProjectManager();

        }
        // GET: StudentProject
        public ActionResult Index()
        {
            if(Session["UserId"] != null && Session["Type"].ToString().Equals("Student"))
            {
                ViewBag.UserId = Session["UserId"];
                return View();
               
            }
            else
            {
                return RedirectToAction("LogIn", "Login", new { message = "Login First.!" });
            }
        }

        [HttpPost]
        public ActionResult Index(ProjectList projectList)
        {
            if (ModelState.IsValid)
            { 
                ProjectList ProjectFiles = new ProjectList();
                
                if (projectList.Slide!=null && projectList.Report != null)
                {
                    ProjectFiles.ProjectSlide = SaveToPhysicalLocation(projectList.Slide);
                    ProjectFiles.ProjectReport = SaveToPhysicalLocation(projectList.Report);
                    ProjectFiles.Status = projectList.Status;
                    ProjectFiles.Id = projectList.Id;
                    ViewBag.msg = ProjectListManager.UpdateFiles(ProjectFiles);
                    
                }
                else
                {
                    ViewBag.msg = "File Error.! please Submit All Required Files.!";
                }
            }
            else
            {
                ViewBag.msg = "Please choice correct files.!";
            }
            return View();
        }

        //File Path generator
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0 && file!=null && file.ContentLength<= 131072)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedImage"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }

        public JsonResult Timeline()
        {
            ProjectList projectList = MyProjectManager.MyProjectTimeline(Convert.ToInt32(Session["UserId"]));
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetComments()
        {
            List<ProjectList> comments = MyProjectManager.GetComments(Convert.ToInt32(Session["UserId"]), "Group");
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        //New Comment
        public string NewComment(int grpId, string comment, string supportName)
        {
            return MyProjectManager.NewComment(grpId, comment, supportName);
        }
    }
}