using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models;
using System.Data.SqlClient;
using System.Data;

namespace PUC_PMS.Gateways
{
    public class LogInGateway:CommonGateway
    {
        //Employee Login

        public LogIn UserLogin(string userName, string type)
        {
            connection.Open();

            // procedure here
            string query = "usp_projectCommon";

            Command = new SqlCommand(query, connection);
            if(type.Equals("Student"))
            { 
                Command.Parameters.AddWithValue("mode", "studentLogin");
                Command.Parameters.AddWithValue("roll", userName);
            }
            else if(type.Equals("Teacher"))
            {
                Command.Parameters.AddWithValue("mode", "teacherLogin");
                Command.Parameters.AddWithValue("roll", userName);
            }
            Command.CommandType = CommandType.StoredProcedure;

            Reader = Command.ExecuteReader();
            LogIn user = new LogIn();
            if (Reader.HasRows)
            { 
                while (Reader.Read())
                {
                    user.Id = Convert.ToInt32(Reader["id"]);
                    user.UserName = Reader["name"].ToString();
                    user.Salt = Convert.ToInt32(Reader["salt"]);
                    user.Password = Reader["password"].ToString();
                    user.Type = type;

                }
            }

            connection.Close();
            return user;
        }
    }
}