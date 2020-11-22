using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PUC_PMS.Managers;
using CrystalDecisions.CrystalReports.Engine;

namespace PUC_PMS.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
       
        ReportDocument doc1 = new ReportDocument();
        ReportManager ReportManager;
        public Reports()
        {
            ReportManager = new ReportManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]); //Convert.ToInt32(Request.QueryString["userId"].ToString());
            int type = Convert.ToInt32(Request.QueryString["type"].ToString());
            string status = Request.QueryString["status"].ToString();


            //string json = Json(projectLists, JsonRequestBehavior.AllowGet);
            DataTable dt = new DataTable();
            if(type == 1)
            {
               int batchName = Convert.ToInt32(Request.QueryString["batch_name"].ToString());
                dt = ReportManager.GetProjectListsindt("all", batchName, 1);
            }
            else if (type == 2)
            {
                dt = ReportManager.GetProjectListsindt("all", userId, 2);
            }
            

            Session["rpt_path"] = "~/Reports/ProjectLstSupervisor.rpt";
            Session["rpt_dt"] = dt;
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            string pppp = Server.MapPath(Session["rpt_path"].ToString());
            doc1.Load(Server.MapPath(Session["rpt_path"].ToString()));
            doc1.SetDataSource((DataTable)Session["rpt_dt"]);
            if (doc1 != null)
            {
                Response.Write("Calling this section");
                CrystalReportViewer1.ReportSource = doc1;
                CrystalReportViewer1.DataBind();
                CrystalReportViewer1.RefreshReport();
                CrystalReportViewer1.Visible = true;
            }
            else
            {
                Response.Write("Error");
            }
        }
        
    }
}