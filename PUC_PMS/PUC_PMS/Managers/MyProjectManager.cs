using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Gateways;
namespace PUC_PMS.Managers
{
    public class MyProjectManager
    {
        private MyProjectGateway MyProjectGateway;
        public MyProjectManager()
        {
            MyProjectGateway = new MyProjectGateway();
        }
        //New Comment
        public string NewComment(int grpId, string comment, string supportName)
        {
            return MyProjectGateway.NewComment(grpId, comment, supportName);
        }
        public ProjectList MyProjectTimeline(int studentId)
        {
            return MyProjectGateway.MyProjectTimeline(studentId);
        }


        //Get Comments bt grp id or student id
        public List<ProjectList> GetComments(int id, string supportName)
        {
            return MyProjectGateway.GetComments(id, supportName);
        }
    }
}