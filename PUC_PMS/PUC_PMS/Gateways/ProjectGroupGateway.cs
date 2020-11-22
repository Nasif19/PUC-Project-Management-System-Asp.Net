using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUC_PMS.Models.ViewModels;
using PUC_PMS.Models;
using System.Data.SqlClient;
using System.Data;

namespace PUC_PMS.Gateways
{
    public class ProjectGroupGateway:CommonGateway
    {
        //Create/Save Project Group
        public string Save(ProjectGroup projectGroup)
        {
            try
            {
                connection.Open();
                string query = "usp_project_group_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("project_group_name", projectGroup.ProjectGroupName);
                Command.Parameters.AddWithValue("status", projectGroup.Status);
                Command.Parameters.AddWithValue("number_of_group", projectGroup.NumberOfGroup);
                Command.Parameters.AddWithValue("batch_name", projectGroup.BatchName);
                Command.Parameters.AddWithValue("tbl_program_id", projectGroup.TblProgramId);

                Command.ExecuteNonQuery();
                connection.Close();
                return "Save Successful.!";

            }
            catch(Exception ex)
            {
                connection.Close();
                return "Save Failed.! Msg: "+ex;
            }
        }

        //Update Project Group
        public string Update(ProjectGroup projectGroup)
        {
            try
            {
                connection.Open();
                string query = "usp_project_group_cu";
                Command = new SqlCommand(query, connection);
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.AddWithValue("id", projectGroup.Id);
                Command.Parameters.AddWithValue("project_group_name", projectGroup.ProjectGroupName);
                Command.Parameters.AddWithValue("status", projectGroup.Status);
                Command.Parameters.AddWithValue("number_of_group", projectGroup.NumberOfGroup);
                Command.Parameters.AddWithValue("batch_name", projectGroup.BatchName);
                Command.Parameters.AddWithValue("tbl_program_id", projectGroup.TblProgramId);

                Command.ExecuteNonQuery();
                connection.Close();
                return "successful.!";

            }
            catch (Exception ex)
            {
                connection.Close();
                return "Failed.! Msg: " + ex;
            }
        }
        
        //Load Groups By Program and Batch
        public List<ProjectGroup> GetAllGroups(string programId, string batch)
        {
            connection.Open();
            string query = "usp_project_group_se";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadGroups");
            Command.Parameters.AddWithValue("tbl_program_id", programId);
            Command.Parameters.AddWithValue("batch_name", batch);
            Reader = Command.ExecuteReader();

            List<ProjectGroup> groups = new List<ProjectGroup>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    ProjectGroup group= new ProjectGroup();

                    group.Id = Convert.ToInt32(Reader["id"]);
                    group.BatchName = Convert.ToInt32(Reader["batch_name"]);
                    group.ProjectGroupName = Reader["project_group_name"].ToString();
                    group.TblProgramId = Convert.ToInt32(Reader["tbl_program_id"]);
                    groups.Add(group);
                }
            }

            connection.Close();
            return groups;
        }
        
        //Load Programs
        public List<DropDownViewModel> GetAllPrograms()
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadAllProgram");
            Reader = Command.ExecuteReader();
            List<DropDownViewModel> Programs = new List<DropDownViewModel>();
            if (Reader.HasRows)
            {
                while(Reader.Read())
                {
                    DropDownViewModel program = new DropDownViewModel();

                    program.Id = Reader["id"].ToString();
                    program.Name = Reader["degreefullname"].ToString();

                    Programs.Add(program);
                }
            }

            connection.Close();
            return Programs;
        }

        public List<DropDownViewModel> GetProgramsByUserId(int userId)
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadProgramsByUserId");
            Command.Parameters.AddWithValue("userId", userId);
            Reader = Command.ExecuteReader();
            List<DropDownViewModel> Programs = new List<DropDownViewModel>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DropDownViewModel program = new DropDownViewModel();

                    program.Id = Reader["id"].ToString();
                    program.Name = Reader["degreefullname"].ToString();

                    Programs.Add(program);
                }
            }

            connection.Close();
            return Programs;
        }

        //Load Batch By Program
        public List<DropDownViewModel> GetAllBatch(string programId)
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadBatchByProgram");
            Command.Parameters.AddWithValue("programId", programId);
            Reader = Command.ExecuteReader();
            List<DropDownViewModel> Batches = new List<DropDownViewModel>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DropDownViewModel batch = new DropDownViewModel();

                    batch.Id = Reader["batchId"].ToString();
                    batch.Name = Reader["batch_name"].ToString();

                    Batches.Add(batch);
                }
            }

            connection.Close();
            return Batches;
        }

        //Load Student By Program and Batch
        public List<DropDownViewModel> GetAllStudents(string programId, string batch)
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadStudentByBatchAndProgramId");
            Command.Parameters.AddWithValue("programId", programId);
            Command.Parameters.AddWithValue("batchName", batch);
            Reader = Command.ExecuteReader();

            List<DropDownViewModel> Students = new List<DropDownViewModel>();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DropDownViewModel student = new DropDownViewModel();

                    student.Id = Reader["id"].ToString();
                    student.Name = Reader["name"].ToString();

                    Students.Add(student);
                }
            }

            connection.Close();
            return Students;
        }

        //Load Teachers By user Id
        public List<DropDownViewModel> GetAllTeachers(int userId)
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadAllTeachers");
            Command.Parameters.AddWithValue("userId", userId);
            Reader = Command.ExecuteReader();
            List<DropDownViewModel> Teachers = new List<DropDownViewModel>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DropDownViewModel teacher = new DropDownViewModel();

                    teacher.Id = Reader["id"].ToString();
                    teacher.Name = Reader["fullname"].ToString();

                    Teachers.Add(teacher);
                }
            }

            connection.Close();
            return Teachers;
        }

        //Load All Department
        public List<DropDownViewModel> GetAllDepartment()
        {
            connection.Open();
            string query = "usp_projectCommon";
            Command = new SqlCommand(query, connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("mode", "loadAllDepartment");
            Reader = Command.ExecuteReader();
            List<DropDownViewModel> Departments = new List<DropDownViewModel>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    DropDownViewModel department = new DropDownViewModel();

                    department.Id = Reader["id"].ToString();
                    department.Name = Reader["department_fullname"].ToString();

                    Departments.Add(department);
                }
            }

            connection.Close();
            return Departments;
        }
    }
}