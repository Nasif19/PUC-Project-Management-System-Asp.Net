using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUC_PMS.Models
{
    public class ProjectGroup
    {
        public int Id { get; set; }
        public string ProjectGroupName { get; set; }
        public int NumberOfGroup { get; set; }
        public int BatchName { get; set; }
        public int TblProgramId { get; set; }
        public string Status { get; set; }
        public int IsDelete { get; set; }

        public int TblProjectGroupId { get; set; }
        public int TblDepartmentId { get; set; }
        public int TblSupervisorId { get; set; }
        public int TblStudentId { get; set; }
        public int SupportId { get; set; }
        public string SupportName { get; set; }
        public string Name { get; set; }
    }
}