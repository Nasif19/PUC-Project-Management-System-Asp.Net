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
    public class ProjectListGateway:CommonGateway
    {
        //Save
        public string Save(ProjectList projectList)
        {
            try
            {
                connection.Open();
                string query = "usp_project_list_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("project_title", projectList.ProjectTite);
                Command.Parameters.AddWithValue("prerequisite_knowledge", projectList.PrerequisiteKnowledge);
                Command.Parameters.AddWithValue("short_description", projectList.ShortDescription);
                Command.Parameters.AddWithValue("version", projectList.Version);
                //Command.Parameters.AddWithValue("publish_type", projectList.PublishType);
                Command.Parameters.AddWithValue("tbl_userinfo_id", projectList.TblUserInfoId);
                Command.Parameters.AddWithValue("using_plartform", projectList.Plartform);
                Command.Parameters.AddWithValue("offered_by", projectList.OfferedBy);
                Command.Parameters.AddWithValue("offer_status", projectList.OfferedStatus);
                Command.Parameters.AddWithValue("tbl_student_id", projectList.TblStudentId);

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

        //Update
        public string Update(ProjectList projectList)
        {
            try
            {
                connection.Open();
                string query = "usp_project_list_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("id", projectList.Id);
                Command.Parameters.AddWithValue("project_title", projectList.ProjectTite);
                Command.Parameters.AddWithValue("prerequisite_knowledge", projectList.PrerequisiteKnowledge);
                Command.Parameters.AddWithValue("short_description", projectList.ShortDescription);
                Command.Parameters.AddWithValue("version", projectList.Version);
                //Command.Parameters.AddWithValue("publish_type", projectList.PublishType);
                Command.Parameters.AddWithValue("tbl_userinfo_id", projectList.TblUserInfoId);
                Command.Parameters.AddWithValue("using_plartform", projectList.Plartform);
                Command.Parameters.AddWithValue("offered_by", projectList.OfferedBy);
                Command.Parameters.AddWithValue("offer_status", projectList.OfferedStatus);

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

        //Update Files
        public string UpdateFiles(ProjectList projectList)
        {
            try
            {
                connection.Open();
                string query = "usp_project_list_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                //Command.Parameters.AddWithValue("mode", "updateFiles");
                Command.Parameters.AddWithValue("id", projectList.Id);
                Command.Parameters.AddWithValue("slide", projectList.ProjectSlide);
                Command.Parameters.AddWithValue("report", projectList.ProjectReport);
                Command.Parameters.AddWithValue("status", projectList.Status);

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
        public List<ProjectList> GetProjectLists(string offeredBy,int userId)
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            if(offeredBy.Equals(""))
            {
                Command.Parameters.AddWithValue("mode", "ProjectListByUserinfoId");
                Command.Parameters.AddWithValue("tbl_userinfo_id", userId);
            }
            else
            {
                Command.Parameters.AddWithValue("mode", "tblDataByOfferStatus");
                Command.Parameters.AddWithValue("tbl_userinfo_id", userId);
                Command.Parameters.AddWithValue("offer_status", offeredBy);
            }
            
            
            Reader = Command.ExecuteReader();

            List<ProjectList> projectLists = new List<ProjectList>();

            if(Reader.HasRows)
            {
                while(Reader.Read())
                {
                    ProjectList projectList = new ProjectList();

                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.PrerequisiteKnowledge = Reader["prerequisite_knowledge"].ToString();
                    projectList.ShortDescription = Reader["short_description"].ToString();
                    projectList.Version = Reader["version"].ToString();
                    projectList.Plartform = Reader["using_plartform"].ToString();
                    projectList.TblUserInfoId = Convert.ToInt32(Reader["tbl_userinfo_id"]);
                    projectList.OfferedBy = Reader["offered_by"].ToString();
                    projectList.OfferedStatus = Reader["offer_status"].ToString();

                    projectLists.Add(projectList);

                }

            }

            connection.Close();
            return projectLists;
        }


        //Get  By id
        public ProjectList GetById(int id)
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
           
            Command.Parameters.AddWithValue("mode", "getById");
            Command.Parameters.AddWithValue("id", id);

            Reader = Command.ExecuteReader();

            ProjectList projectList = new ProjectList();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    projectList.Id = Convert.ToInt32(Reader["id"]);
                    projectList.ProjectTite = Reader["project_title"].ToString();
                    projectList.PrerequisiteKnowledge = Reader["prerequisite_knowledge"].ToString();
                    projectList.ShortDescription = Reader["short_description"].ToString();
                    projectList.Version = Reader["version"].ToString();
                    projectList.Plartform = Reader["using_plartform"].ToString();
                    //projectList.TblUserInfoId = Convert.ToInt32(Reader["tbl_userinfo_id"]);
                    projectList.OfferedBy = Reader["offered_by"].ToString();
                    projectList.OfferedStatus = Reader["offer_status"].ToString();

                }
            }
            connection.Close();
            return projectList;
        }


        //Get Offered By
        public List<ProjectList> GetAllOfferedStatus()
        {
            connection.Open();
            string query = "usp_project_list_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.AddWithValue("mode", "OfferedStatus");


            Reader = Command.ExecuteReader();

            List<ProjectList> offerStatuss = new List<ProjectList>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectList offerStatus = new ProjectList();

                    offerStatus.OfferedStatus = Reader["offer_status"].ToString();
                    offerStatuss.Add(offerStatus);
                }
            }


            connection.Close();
            return offerStatuss;
        }

        //Delete
        public string Delete(int id)
        {
            try
            {
                connection.Open();
                string query = "usp_project_list_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("id", id);
                Command.Parameters.AddWithValue("mode", "delete");


                Command.ExecuteNonQuery();
                connection.Close();
                return "Delete Successful.!";

            }
            catch (Exception ex)
            {
                connection.Close();
                return "Delete Failed.! Msg: " + ex;
            }
        }


        
    }
}