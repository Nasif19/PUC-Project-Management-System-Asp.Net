using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using PUC_PMS.Models;
using PUC_PMS.Models.ViewModels;

namespace PUC_PMS.Gateways
{
    public class SupervisorGateway:CommonGateway
    {
        //Save
        public string Save(ProjectGroup projectGroup)
        {
            try
            {
                connection.Open();
                string query = "usp_project_group_supervisors_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("tbl_userinfo_id", projectGroup.SupportId);
                Command.Parameters.AddWithValue("tbl_project_group_id", projectGroup.TblProjectGroupId);
                

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

        //Get Supervisor by group Id
        public List<ProjectGroup> GetSupervisrByGroupId(int groupId)
        {
            connection.Open();
            string query = "usp_project_group_supervisors_se";

            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.AddWithValue("tbl_project_group_id", groupId);
            Reader = Command.ExecuteReader();
            List<ProjectGroup> Names = new List<ProjectGroup>();
            if(Reader.HasRows)
            {
                while(Reader.Read())
                {
                    ProjectGroup name = new ProjectGroup();
                   
                    name.Id = Convert.ToInt32(Reader["id"]);
                    name.Name = Reader["name"].ToString();

                    Names.Add(name);
                }
            }
            connection.Close();
            return Names;
        }

        //Delete
        public string Remove(int id)
        {
            try
            {
                connection.Open();
                string query = "usp_project_group_supervisors_cu";

                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("mode","delete");
                Command.Parameters.AddWithValue("id", id);
                Command.ExecuteNonQuery();

                connection.Close();
                return "Successful.!";
            }
            catch(Exception ex)
            {
                connection.Close();
                return "Faield.! Ex- " + ex;
            }
        }
    }
}