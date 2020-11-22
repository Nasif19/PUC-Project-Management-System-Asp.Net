using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using PUC_PMS.Models.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace PUC_PMS.Gateways
{
    public class ReportGateway:CommonGateway
    {
        //Get Project List By Offered status or user id
        public List<ProjectList> GetProjectLists(string status, int userId, int type)
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            if (type == 2)
            {
                Command.Parameters.AddWithValue("mode", "bySupervisorIdAbdStatus");
                Command.Parameters.AddWithValue("tbl_userinfo_id", userId);
                Command.Parameters.AddWithValue("status", status);
            }
            else if (type == 1)
            {
                Command.Parameters.AddWithValue("mode", "bybatchIdAbdStatus");
                Command.Parameters.AddWithValue("batch_name", userId);
                Command.Parameters.AddWithValue("status", status);
            }


            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.GroupName = Reader["project_group_name"].ToString();
                    projectList.SupervisorName = Reader["fullname"].ToString();

                    if (Reader["status"].ToString().Equals("Accepted"))
                    {
                        projectList.Status = "Running";
                    }
                    else
                    {
                        projectList.Status = Reader["status"].ToString();
                    }

                    projectList.BatchId = Convert.ToInt32(Reader["batch_name"]);

                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }

        public DataTable GetProjectListsindt(string status, int userId, int type)
        {
            connection.Open();
            string query = "usp_project_report";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            if (type == 2)
            {
                //Command.Parameters.AddWithValue("mode", "bySupervisorIdAbdStatus");
                Command.Parameters.AddWithValue("tbl_userinfo_id", userId);
                Command.Parameters.AddWithValue("status", status);
            }
            else if (type == 1)
            {
                Command.Parameters.AddWithValue("mode", "bybatchIdAbdStatus");
                Command.Parameters.AddWithValue("batch_name", userId);
                Command.Parameters.AddWithValue("status", status);
            }

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(Command);
            sda.Fill(dt);

            connection.Close();
            return dt;
        }
    }
}