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
    public class StudentProjectGateway:CommonGateway
    {
        //Project Request
        public string Save(int studentId, int projectListId)
        {
            try
            {
                connection.Open();
                string query = "usp_project_request_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("tbl_student_id", studentId);
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
        //ProjectListGateway have Save and Update method.

        //Get Project List By Offered status or user id
        public List<ProjectList> GetProjectLists(string offeredBy, int studentId)
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            if (offeredBy.Equals(""))
            {
                Command.Parameters.AddWithValue("mode", "ProjectListByStudentId");
                Command.Parameters.AddWithValue("tbl_student_id", studentId);
            }
            else
            {
                Command.Parameters.AddWithValue("mode", "ProjectListByStudentIdAndOfferStatus");
                Command.Parameters.AddWithValue("tbl_student_id", studentId);
                Command.Parameters.AddWithValue("offer_status", offeredBy);
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
                    projectList.PrerequisiteKnowledge = Reader["prerequisite_knowledge"].ToString();
                    projectList.ShortDescription = Reader["short_description"].ToString();
                    projectList.Version = Reader["version"].ToString();
                    projectList.Plartform = Reader["using_plartform"].ToString();
                    //projectList.TblUserInfoId = Convert.ToInt32(Reader["tbl_userinfo_id"]);
                    projectList.OfferedBy = Reader["offered_by"].ToString();
                    projectList.OfferedStatus = Reader["offer_status"].ToString();

                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }

        //Get Project List By Offered status or user id
        public List<ProjectList> GetStudentProjectLists(int studentId)
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
           
            Command.Parameters.AddWithValue("mode", "ProjectListByStudent");
            Command.Parameters.AddWithValue("tbl_student_id", studentId);
           

            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.PrerequisiteKnowledge = Reader["prerequisite_knowledge"].ToString();
                    projectList.ShortDescription = Reader["short_description"].ToString();
                    projectList.Version = Reader["version"].ToString();
                    projectList.Plartform = Reader["using_plartform"].ToString();
                    //projectList.TblUserInfoId = Convert.ToInt32(Reader["tbl_userinfo_id"]);
                    projectList.OfferedBy = Reader["offered_by"].ToString();
                    projectList.OfferedStatus = Reader["offer_status"].ToString();
                    projectList.Status = Reader["status"].ToString();

                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }
        //ProjectListGateway has Get  By id method


    }
}