using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Models;
using PUC_PMS.Gateways;
using PUC_PMS.Managers;

namespace PUC_PMS.Controllers
{
    public class StudentProjectController : Controller
    {
        private ProjectListManager ProjectListManager;
        private StudentProjectManager StudentProjectManager;
        public StudentProjectController()
        {
            ProjectListManager = new ProjectListManager();
            StudentProjectManager = new StudentProjectManager();

        }
        // GET: StudentProject
        public ActionResult Index()
        {
            if (Session["UserId"] != null && Session["Type"].ToString().Equals("Student"))
            {
                ViewBag.UserId = Session["UserId"];
                ViewBag.ProjectList = StudentProjectManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                ViewBag.OfferedByList = GetAllOfferedBy();
                return View();

            }
            else
            {
                return RedirectToAction("LogIn", "Login", new { message = "Login First.!" });
            }
            
        }


        [HttpPost]
        public ActionResult Index(ProjectList projectList, string submitButton)
        {
            if (ModelState.IsValid)
            {
                if (submitButton.Equals("Save"))
                {
                    ViewBag.Response = ProjectListManager.Save(projectList);
                    ModelState.Clear();
                    ViewBag.ProjectList = StudentProjectManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }
                if (submitButton.Equals("Update"))
                {
                    ViewBag.Response = ProjectListManager.Update(projectList);
                    ModelState.Clear();
                    ViewBag.ProjectList = StudentProjectManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }
                else if (submitButton.Equals("delete"))
                {
                    ViewBag.Response = ProjectListManager.Delete(projectList.Id);
                    ViewBag.ProjectList = StudentProjectManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }

                else
                {
                    ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }
            }
            else
            {

                ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                ViewBag.OfferedByList = GetAllOfferedBy();
                ViewBag.Response = "Fill up all fields correctly";
                return View();
            }
        }

        //Project Request
        public string RequestProject(int studentId, int projectListId)
        {
            return StudentProjectManager.Save(studentId, projectListId);
        }

        //Get All Offered By
        public List<SelectListItem> GetAllOfferedBy()
        {
            List<ProjectList> OfferedBy = ProjectListManager.GetAllOfferedStatus();
            List<SelectListItem> OfferedByList = OfferedBy.ConvertAll(x => new SelectListItem()
            {
                Text = x.OfferedStatus,
                Value = x.OfferedStatus
            });
            return OfferedByList;
        }

        // Get All Project List
        public JsonResult GetProjectList(string offerStatus, int studentId)
        {
            List<ProjectList> ProjectList = StudentProjectManager.GetProjectLists(offerStatus, studentId);
            return Json(ProjectList, JsonRequestBehavior.AllowGet);
        }

        // Get All student Project List
        public JsonResult GetStudentProjectList(int studentId)
        {
            List<ProjectList> ProjectList = StudentProjectManager.GetStudentProjectLists(studentId);
            return Json(ProjectList, JsonRequestBehavior.AllowGet);
        }
    }
}