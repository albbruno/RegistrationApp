using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using University;

namespace RegistrationApp_Web_.Models
{
    public class SignIn
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public bool InfoCheck(string name, string pass)
        {
            return CheckUserInfo(name, pass);
        }

        public Student GetStudent(int id)
        {
            bool check = false;
            string qry = $"SELECT * FROM Student WHERE StudentId = {id}";
            Student std = null;
            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();

                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        std = new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());

                        //std = new Student(CurrentUser.Username, CurrentUser.Password, "", (int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());
                    }
                    dReader.Close();
                    sqlcon.Close();
                    check = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    check = false;
                }

            }

            return std;
        }

        public bool CheckUserInfo(string username, string password)
        {
            bool infoCorrect = false;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand($"LoginInfoCheck", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                cmd.Parameters.Add(new SqlParameter("@password", password));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                
                try
                {
                    sqlcon.Open();
                    cmd.ExecuteNonQuery(); //int num = 
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    infoCorrect = (bool)returnValue.Value;

                    sqlcon.Close();

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"The issue is: {ex.Message}");
                    infoCorrect = false;

                }

            }

            return infoCorrect;
        }

        public int GetUserId(string username)
        {
            int id = -1;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand($"GetUserId", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@username", username));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                //cmd.Parameters.Add("@password", SqlDbType.VarChar);

                //cmd.Parameters["@username"].Value = username;
                //cmd.Parameters["@password"].Value = password;

                try
                {

                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    id = (int)returnValue.Value;
                    sqlcon.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"The issue is: {ex.Message}");
                    id = -1;
                }

            }

            return id;
        }

        public int GetStudentId(int userId)
        {
            int id = -1;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand($"GetStudentId", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                //cmd.Parameters.Add("@password", SqlDbType.VarChar);
                //cmd.Parameters["@username"].Value = username;
                //cmd.Parameters["@password"].Value = password;

                try
                {
                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    id = (int)returnValue.Value;
                    sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The issue is: {ex.Message}");
                    id = -1;
                }

            }

            return id;
        }

    }
}
