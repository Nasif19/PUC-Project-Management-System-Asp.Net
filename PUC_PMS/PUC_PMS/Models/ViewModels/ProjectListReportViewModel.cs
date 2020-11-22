using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUC_PMS.Models.ViewModels
{
    public class ProjectListReportViewModel
    {
        public string ReportType { get; set; }
        public string status { get; set; }
        public string BatchId { get; set; }
        public string SupervisorId { get; set; }
    }
}