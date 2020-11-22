using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Models;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class ProjectGroupManager
    {
        private ProjectGroupGateway ProjectGroupGateway;

        public ProjectGroupManager()
        {
            ProjectGroupGateway = new ProjectGroupGateway();
        }

        //Create/Save Group
        public string Save(ProjectGroup projectGroup)
        {
            return ProjectGroupGateway.Save(projectGroup);
        }

        //Get Group By program and batch
        public List<ProjectGroup> GetAllGroups(string programId, string batch)
        {
            return ProjectGroupGateway.GetAllGroups(programId, batch);
        }

        //Update Group
        public string Update(ProjectGroup projectGroup)
        {
            return ProjectGroupGateway.Update(projectGroup);
        }

        //Load Program by user id
        public List<DropDownViewModel> GetProgramsByUserId(int userId)
        {
            return ProjectGroupGateway.GetProgramsByUserId(userId);
        }

        //Load all Programs
        public List<DropDownViewModel> GetAllPrograms()
        {
            return ProjectGroupGateway.GetAllPrograms();
        }

        //Load All Batch by programs
        public List<DropDownViewModel> GetAllBatch(string programId)
        {
            return ProjectGroupGateway.GetAllBatch(programId);
        }

        //Load All Students by program and Batch
        public List<DropDownViewModel> GetAllStudents(string programId, string batch)
        {
            return ProjectGroupGateway.GetAllStudents(programId, batch);
        }

        //Load Teachers by user id
        public List<DropDownViewModel> GetAllTeachers(int userId)
        {
            return ProjectGroupGateway.GetAllTeachers(userId);
        }

        //Load All Department
        public List<DropDownViewModel> GetAllDepartment()
        {
            return ProjectGroupGateway.GetAllDepartment();
        }
    }
}