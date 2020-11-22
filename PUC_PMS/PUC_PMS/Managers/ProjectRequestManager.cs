using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Models.ViewModels;
using System.Data.SqlClient;
using System.Data;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class ProjectRequestManager
    {
        private ProjectRequestGateway ProjectRequestGateway;
        public ProjectRequestManager()
        {
            ProjectRequestGateway = new ProjectRequestGateway();
        }
        //update
        public string UpdateRequest(string status, int projectGrpId, int projectListId)
        {
            return ProjectRequestGateway.UpdateRequest(status, projectGrpId, projectListId);
        }
        public List<ProjectList> GetProjectGroups(int userId)
        {
            return ProjectRequestGateway.GetProjectGroups(userId);
        }

        //PRoject List
        public List<ProjectList> GetProjectList(int groupId)
        {
            return ProjectRequestGateway.GetProjectList(groupId);
        }
    }
}