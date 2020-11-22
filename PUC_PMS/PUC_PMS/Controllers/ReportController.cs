using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUC_PMS.Managers;
using PUC_PMS.Models;
using PUC_PMS.Models.ViewModels;

namespace PUC_PMS.Controllers
{
    public class ReportController : Controller
    {
        private ProjectListManager ProjectListManager;
        ProjectGroupManager ProjectGroupManager;
        ReportManager ReportManager;
        public ReportController()
        {
            ProjectListManager = new ProjectListManager();
            ProjectGroupManager = new ProjectGroupManager();
            ReportManager = new ReportManager();
        }
        // GET: Report
        public ActionResult Index()
        {
            if (Session["UserId"] != null && Session["Type"].ToString().Equals("Teacher"))
            {
                //ViewBag.UserId = Session["UserId"];
                //ViewBag.ProjectList = ProjectListManager.GetProjectLists("", Convert.ToInt32(Session["UserId"]));
                //ViewBag.OfferedByList = GetAllOfferedBy();
                ViewBag.Batches = GetAllBatch();
                ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Login", new { message = "Login First.!" });
            }
        }

        //Get Program By Id
        public List<SelectListItem> GetAllBatch()
        {
            List<DropDownViewModel> program = ProjectGroupManager.GetProgramsByUserId(Convert.ToInt32(Session["UserId"]));

            List<DropDownViewModel> Batches = ProjectGroupManager.GetAllBatch(program[0].Id);
            List<SelectListItem> selectListItems = Batches.ConvertAll(x => new SelectListItem()
            { Text = x.Name, Value = x.Id.ToString() });
            return selectListItems;
        }

        //Get All supervisor for DropDown
        public List<SelectListItem> GetAllTeachersbyUserId(int userId)
        {
            List<DropDownViewModel> Teachers = ProjectGroupManager.GetAllTeachers(userId);
            List<SelectListItem> selectListItems = Teachers.ConvertAll(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return selectListItems;
        }


        //Get Project List By Offered status or user id
        public JsonResult GetProjectLists(string status, int userId, int type)
        //public JsonResult GetProjectLists(ProjectListReportViewModel projectListReportViewModel, int type)
        {
           List<ProjectList> projectLists = ReportManager.GetProjectLists(status,userId, type);
            return Json(projectLists, JsonRequestBehavior.AllowGet);
            
        }

        //[HttpPost]
        //public ActionResult Index(string submitButton)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (submitButton.Equals("submitReport"))
        //        {
        //            ViewBag.Response = ProjectGroupManager.Save(projectGroup);
        //            ModelState.Clear();
        //            ViewBag.Programs = GetProgramsForDropDown();
        //            //ViewBag.Departments = GetAllDepartment();
        //            ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
        //            return View(projectGroup);
        //        }
        //    }
        //}
    }
}