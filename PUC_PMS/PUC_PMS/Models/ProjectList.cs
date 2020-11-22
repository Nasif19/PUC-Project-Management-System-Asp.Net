using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUC_PMS.Models
{
    public class ProjectList
    {

        public int Id { get; set; }
        public string ProjectTite { get; set; }
        public string PrerequisiteKnowledge { get; set; }
        public string ShortDescription { get; set; }
        public string PublishType { get; set; }
        public string Version { get; set; }
        public string Plartform { get; set; }
        public int TblProjectGroupId { get; set; }
        public int TblUserInfoId { get; set; }
        public int TblStudentId { get; set; }
        public string OfferedBy { get; set; }
        public string OfferedStatus { get; set; }
        public string Status { get; set; }
        public string GroupName { get; set; }
        public string SupervisorName { get; set; }
        public string Comment { get; set; }
        public int Total { get; set; }
        public int BatchId { get; set; }
        public int SupervisorId { get; set; }
        public HttpPostedFileBase Slide { get; set; }
        public HttpPostedFileBase Report { get; set; }
        public string ProjectSlide { get; set; }
        public string ProjectReport { get; set; }
        //public string ProjectSlide { get; set; }
        //public string ProjectReport { get; set; }
        //public string FinalProjectSlide { get; set; }
        //public string FinalProjectReport { get; set; }

        /*
        proposal_presentation_file	nvarchar(MAX)	Checked
        proposal_report	nvarchar(MAX)	Checked
        project_presentation_file	nvarchar(MAX)	Checked
        project_report	nvarchar(MAX)	Checked
        project_file	nvarchar(MAX)	Checked
        using_plartform	varchar(200)	Checked
        status	varchar(50)	Checked
        is_delete	int	Checked
		Unchecked*/
    }
}