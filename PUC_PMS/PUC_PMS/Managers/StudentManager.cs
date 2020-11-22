using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Models;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class StudentManager
    {
        private StudentGateway StudentGateway;
        public StudentManager()
        {
            StudentGateway = new StudentGateway();
        }


        //Add Supervisor
        public string AddStudent(ProjectGroup projectGroup)
        {
            return StudentGateway.Save(projectGroup);
        }

        //Get Supervisor by Group Id
        public List<ProjectGroup> GetStudentByGroupId(int groupId)
        {
            return StudentGateway.GetSupervisrByGroupId(groupId);
        }

        //delete
        public string Remove(int id)
        {
            return StudentGateway.Remove(id);
        }
    }
}