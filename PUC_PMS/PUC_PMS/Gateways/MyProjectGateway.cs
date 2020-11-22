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
    public class MyProjectGateway:CommonGateway
    {
        //New Comment
        public string NewComment(int grpId, string comment, string supportName)
        {
            try
            {
                connection.Open();
                string query = "usp_project_comments_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("comment", comment);
                Command.Parameters.AddWithValue("tbl_project_group_id", grpId);
                Command.Parameters.AddWithValue("support_name", supportName);
                
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
        //Get  By id
        public ProjectList MyProjectTimeline(int studentId)
        {
            connection.Open();
            string query = "usp_projectCommon";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.AddWithValue("mode", "myProjectTimeline");
            Command.Parameters.AddWithValue("tbl_student_id", studentId);

            Reader = Command.ExecuteReader();

            ProjectList projectList = new ProjectList();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.GroupName = Reader["project_group_name"].ToString();
                    if(Reader["status"].ToString().Equals("Accepted"))
                    {
                        projectList.Status = "Running";
                    }
                    else
                    {
                        projectList.Status = Reader["status"].ToString();
                    }
                    
                    projectList.SupervisorName = Reader["fullname"].ToString();
                    
                }
            }
            else
            {
                projectList.Status = "No Project Accepted Yet.! <br/>" +
                    " Or <br/> Please Request a Project.!";
            }
            connection.Close();
            return projectList;
        }

        //Get Comments bt grp id or student id
        public List<ProjectList> GetComments(int id,string supportName)
        {
            connection.Open();
            string query = "usp_project_comments_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
          
            Command.Parameters.AddWithValue("mode", "comment");
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("support_name", supportName);



            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.Comment = Reader["comment"].ToString();
                    projectList.GroupName = Reader["support_name"].ToString();
                    
                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }


    }
}