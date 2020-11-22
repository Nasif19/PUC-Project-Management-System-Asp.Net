using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Models;
using PUC_PMS.Gateways;

namespace PUC_PMS.Managers
{
    public class SupervisorManager
    {

        private SupervisorGateway supervisorGateway;
        public SupervisorManager()
        {
            supervisorGateway = new SupervisorGateway();
        }


        //Add Supervisor
        public string AddSupervisor(ProjectGroup projectGroup)
        {
            return supervisorGateway.Save(projectGroup);
        }

        //Get Supervisor by Group Id
        public List<ProjectGroup> GetSupervisorByGroupId(int groupId)
        {
            return supervisorGateway.GetSupervisrByGroupId(groupId);
        }

        //delete
        public string Remove(int id)
        {
            return supervisorGateway.Remove(id);
        }
    }
}