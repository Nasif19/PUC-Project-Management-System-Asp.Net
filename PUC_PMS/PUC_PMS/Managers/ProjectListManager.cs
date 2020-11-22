using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class ProjectListManager
    {
        private ProjectListGateway ProjectListGateway;
        public ProjectListManager()
        {
            ProjectListGateway = new ProjectListGateway();
        }

        //save
        public string Save(ProjectList projectList)
        {
            return ProjectListGateway.Save(projectList);
        }

        //Update
        public string Update(ProjectList projectList)
        {
            return ProjectListGateway.Update(projectList);
        }
        //Update Files
        public string UpdateFiles(ProjectList projectList)
        {
            return ProjectListGateway.UpdateFiles(projectList);
        }
        //Get Project List By Offered Status or user Id
        public List<ProjectList> GetProjectLists(string offeredBy, int userId)
        {
            return ProjectListGateway.GetProjectLists(offeredBy, userId);
        }

        public List<ProjectList> GetAllOfferedStatus()
        {
            return ProjectListGateway.GetAllOfferedStatus();
        }

        //Get By Id
        public ProjectList GetById(int id)
        {
            return ProjectListGateway.GetById(id);
        }
        //Delete
        public string Delete(int id)
        {
            return ProjectListGateway.Delete(id);
        }
    }
}