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
    public class ProjectListController : Controller
    {
        private ProjectListManager ProjectListManager;
        public ProjectListController()
        {
            ProjectListManager = new ProjectListManager();

        }
        // GET: ProjectManage
        public ActionResult Index()
        {
            if (Session["UserId"] != null && Session["Type"].ToString().Equals("Teacher"))
            {
                ViewBag.UserId = Session["UserId"];
                ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
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
            if(ModelState.IsValid)
            {
                if (submitButton.Equals("Save"))
                {
                    ViewBag.Response = ProjectListManager.Save(projectList);
                    ModelState.Clear();
                    ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }
                if (submitButton.Equals("Update"))
                {
                    ViewBag.Response = ProjectListManager.Update(projectList);
                    ModelState.Clear();
                    ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                    ViewBag.OfferedByList = GetAllOfferedBy();
                    return View(projectList);
                }
                else if(submitButton.Equals("delete"))
                {
                    ViewBag.Response = ProjectListManager.Delete(projectList.Id);
                    ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
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
        public JsonResult GetProjectList(string offerStatus,int userId)
        {
            List<ProjectList> ProjectList = ProjectListManager.GetProjectLists(offerStatus,userId);
            return Json(ProjectList, JsonRequestBehavior.AllowGet);
        }

        // Get Project List By Id
        public JsonResult GetById(int id)
        {
            ProjectList ProjectList = ProjectListManager.GetById(id);
            return Json(ProjectList, JsonRequestBehavior.AllowGet);
        }

    }
}