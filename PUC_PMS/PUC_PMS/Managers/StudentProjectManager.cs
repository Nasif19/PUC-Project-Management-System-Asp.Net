using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class StudentProjectManager
    {
        private StudentProjectGateway StudentProjectGateway;
        public StudentProjectManager()
        {
            StudentProjectGateway = new StudentProjectGateway();
        }

        //Project Request
        public string Save(int studentId, int projectListId)
        {
            return StudentProjectGateway.Save(studentId, projectListId);

        }
        //Get Project List By Offered Status or student Id
        public List<ProjectList> GetProjectLists(string offeredBy, int studentId)
        {
            return StudentProjectGateway.GetProjectLists(offeredBy, studentId);
        }

        //Get Student Project List By Offered Status or student Id
        public List<ProjectList> GetStudentProjectLists( int studentId)
        {
            return StudentProjectGateway.GetStudentProjectLists(studentId);
        }

    }
}