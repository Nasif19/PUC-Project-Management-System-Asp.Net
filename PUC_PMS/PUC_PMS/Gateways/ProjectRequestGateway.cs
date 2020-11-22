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
    public class ProjectRequestGateway:CommonGateway
    {

        //Update Request
        public string UpdateRequest(string status, int projectGrpId, int projectListId)
        {
            try
            {
                connection.Open();
                string query = "usp_project_request_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("mode", "status");
                Command.Parameters.AddWithValue("status", status);
                Command.Parameters.AddWithValue("tbl_project_group_id", projectGrpId);
                Command.Parameters.AddWithValue("tbl_project_list_id", projectListId);


                Command.ExecuteNonQuery();
                connection.Close();
                return "Save Successful.!";

            }
            catch (Exception ex)
            {
                connection.Close();
                return "Save Failed.! Msg: " + ex;
            }
        }

        //Get Project List By Offered status or user id
        public List<ProjectList> GetProjectGroups(int userId)
        {
            connection.Open();
            string query = "usp_project_request_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
          
            Command.Parameters.AddWithValue("mode", "totalProjectRequest");
            Command.Parameters.AddWithValue("tbl_userinfo_id", userId);
            


            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.GroupName = Reader["project_group_name"].ToString();
                    projectList.Total = Convert.ToInt32(Reader["total"]);
                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }

        //Get Project List By Offered status or user id
        public List<ProjectList> GetProjectList(int groupId)
        {
            connection.Open();
            string query = "usp_project_request_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.AddWithValue("mode", "projectList");
            Command.Parameters.AddWithValue("tbl_project_group_id", groupId);



            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.Status = Reader["status"].ToString();
                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }
    }
}