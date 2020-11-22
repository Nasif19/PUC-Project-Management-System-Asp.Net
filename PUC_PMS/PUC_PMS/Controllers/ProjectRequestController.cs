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
    public class ProjectRequestController : Controller
    {
        private ProjectRequestManager ProjectRequestManager;
        private ProjectListManager projectListManager;
        private MyProjectManager MyProjectManager;
        public ProjectRequestController()
        {
            ProjectRequestManager = new ProjectRequestManager();
            projectListManager = new ProjectListManager();
            MyProjectManager = new MyProjectManager();
        }
        // GET: ProjectRequest
        public ActionResult Index()
        {
            if (Session["UserId"] != null && Session["Type"].ToString().Equals("Teacher"))
            {
                ViewBag.UserId = Session["UserId"];
                ViewBag.OfferedByList = GetAllOfferedBy();
                ViewBag.ProjectGroup = ProjectRequestManager.GetProjectGroups(Convert.ToInt32(Session["UserId"]));
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Login", new { message = "Login First.!" });
            }
            
        }

        // Get Project List
        public JsonResult GetProjectList(int groupId)
        {
            List<ProjectList> ProjectList = ProjectRequestManager.GetProjectList(groupId);
            return Json(ProjectList, JsonRequestBehavior.AllowGet);
        }
        //Get All Offered By
        public List<SelectListItem> GetAllOfferedBy()
        {
            List<ProjectList> OfferedBy = projectListManager.GetAllOfferedStatus();
            List<SelectListItem> OfferedByList = OfferedBy.ConvertAll(x => new SelectListItem()
            {
                Text = x.OfferedStatus,
                Value = x.OfferedStatus
            });
            return OfferedByList;
        }
        //update
        public string UpdateRequest(string status, int projectGrpId, int projectListId)
        {
            return ProjectRequestManager.UpdateRequest(status, projectGrpId, projectListId);
        }
        public JsonResult GetComments(int groupId)
        {
            List<ProjectList> comments = MyProjectManager.GetComments(groupId, "Supervisor");
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        //New Comment
        public string NewComment(int grpId, string comment, string supportName)
        {
            return MyProjectManager.NewComment(grpId, comment, supportName);
        }
    }
}