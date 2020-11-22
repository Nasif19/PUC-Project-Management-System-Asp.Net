using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Gateways;
using PUC_PMS.Models.ViewModels;
using System.Data;

namespace PUC_PMS.Managers
{
    public class ReportManager
    {
        ReportGateway ReportGateway;
        public ReportManager()
        {
            ReportGateway = new ReportGateway();
        }

        //Get Project List By Offered status or user id
        public List<ProjectList> GetProjectLists(string status, int userId, int type)
        {
            return ReportGateway.GetProjectLists(status, userId, type);
        }

        public DataTable GetProjectListsindt(string status, int userId, int type)
        {
            return ReportGateway.GetProjectListsindt(status, userId, type);
        }

    }
}