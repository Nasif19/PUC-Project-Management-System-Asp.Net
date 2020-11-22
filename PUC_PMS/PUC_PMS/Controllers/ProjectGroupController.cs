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
    public class ProjectGroupController:Controller
    {
        private ProjectGroupManager ProjectGroupManager;
        private SupervisorManager SupervisorManager;
        private StudentManager StudentManager;
        public ProjectGroupController()
        {
            ProjectGroupManager = new ProjectGroupManager();
            SupervisorManager = new SupervisorManager();
            StudentManager = new StudentManager();
        }

        
        public ActionResult Index()
        {
            if (Session["UserId"] != null && Session["Type"].ToString().Equals("Teacher"))
            {
                ViewBag.Programs = GetProgramById(Convert.ToInt32(Session["UserId"]));
                ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                return View();

            }
            else
            {
                return RedirectToAction("LogIn", "Login", new { message = "Login First.!" });
            }
            
        }

        [HttpPost]
        public ActionResult Index(ProjectGroup projectGroup, string submitButton)
        {
            if (ModelState.IsValid)
            {
                if (submitButton.Equals("Create"))
                {
                    ViewBag.Response = ProjectGroupManager.Save(projectGroup);
                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    //ViewBag.Departments = GetAllDepartment();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }
                //for Supervisor
                else if(submitButton.Equals("AddSupervisor"))
                {
                    ViewBag.Response = SupervisorManager.AddSupervisor(projectGroup);
                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    //ViewBag.Departments = GetAllDepartment();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }
                else if (submitButton.Equals("RemoveSupervisor"))
                {
                    ViewBag.Response = RemoveSupervisor(projectGroup.Id);
                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    //ViewBag.Departments = GetAllDepartment();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }

                //for student
                else if (submitButton.Equals("Addstudent"))
                {
                    ViewBag.Response = StudentManager.AddStudent(projectGroup);
                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }
                else if (submitButton.Equals("RemoveStudent"))
                {
                    ViewBag.Response = RemoveStudent(projectGroup.Id);
                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }

                else 
                {

                    ModelState.Clear();
                    ViewBag.Programs = GetProgramsForDropDown();
                    ViewBag.Supervisors = GetAllTeachersbyUserId(Convert.ToInt32(Session["UserId"]));
                    return View(projectGroup);
                }

            }
            else
            {
                ViewBag.Response = "Fill up all fields correctly";
                ViewBag.Programs = GetProgramsForDropDown();
                //ViewBag.Departments = GetAllDepartment();
                return View();
            }
        }

       //Get All Groups
       public JsonResult GetGroupList(string programId, string batch)
        {
            List<ProjectGroup> GroupList = ProjectGroupManager.GetAllGroups(programId, batch);
            return Json(GroupList, JsonRequestBehavior.AllowGet);
        }

        //Get Program By Id
        public List<SelectListItem> GetProgramById(int programId)
        {
            List<DropDownViewModel> programs = ProjectGroupManager.GetProgramsByUserId(programId);
            List<SelectListItem> selectListItems = programs.ConvertAll(x => new SelectListItem()
            { Text = x.Name, Value = x.Id.ToString() });
            return selectListItems;
        }

        //All Programs
        public List<SelectListItem> GetProgramsForDropDown()
        {
            List<DropDownViewModel> programs = ProjectGroupManager.GetAllPrograms();
            List<SelectListItem> selectListItems = programs.ConvertAll(x => new SelectListItem()
                { Text=x.Name, Value=x.Id.ToString()});
            return selectListItems;
        }

        //All Batch
        public JsonResult GetBatchList(string programId)
        {
            List<DropDownViewModel> BatchList = ProjectGroupManager.GetAllBatch(programId);
            return Json(BatchList, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetBatchesForDropDown(string programId)
        {
            List<DropDownViewModel> Batches = ProjectGroupManager.GetAllBatch(programId);
            List<SelectListItem> selectListItems = Batches.ConvertAll(x => new SelectListItem()
                {Text = x.Name, Value = x.Id.ToString()});
            return selectListItems;
        }

        //All Students
        public JsonResult GetStudentsForDropDown(string programId, string batch)
        {
            List<DropDownViewModel> Students = ProjectGroupManager.GetAllStudents(programId, batch);
            
            return Json(Students, JsonRequestBehavior.AllowGet);
        }

        //Get All Teacher and Student by group Id

        public JsonResult GetSupervisorAndStudentList(int groupId,string type)
        {
            List<ProjectGroup> list = new List<ProjectGroup>();
            if (type.Equals("supervisor"))
            { 
                list = SupervisorManager.GetSupervisorByGroupId(groupId);
            }
            else
            {
                list = StudentManager.GetStudentByGroupId(groupId);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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

        ////All Department
        //List<SelectListItem> GetAllDepartment()
        //{
        //    List<DropDownViewModel> Departments = ProjectGroupManager.GetAllDepartment();
        //    List<SelectListItem> selectListItems = Departments.ConvertAll(x => new SelectListItem()
        //    {
        //        Text = x.Name, Value = x.Id.ToString()
        //    });
        //    return selectListItems;
        //}

        public string RemoveSupervisor(int id)
        {
            return SupervisorManager.Remove(id);
        }
        public string RemoveStudent(int id)
        {
            return StudentManager.Remove(id);
        }
    }
}