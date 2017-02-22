using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using University;
using System.Data;

namespace DatabaseLib
{
    public static class ConnectionObj
    {

        public static string connectionStr = ConfigurationManager.ConnectionStrings["RegAppConnection"].ConnectionString;

        //private static ConnectionObj()
        //{
        //    //connectionStr = ConfigurationManager.ConnectionStrings/["RegAppConnection"].ConnectionString;
        //}
        
        public static void OpenSqlConnection()
        {
            string connectionString = connectionStr;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                Console.WriteLine("State {0}", connection.State);
                Console.WriteLine($"State: {connection.State}");
                Console.WriteLine("Connectionstring {0}", connection.ConnectionString);
            }

        }

        public static void GetStudentTableContents(string query)
        {
            string qry = $"SELECT * FROM Student";
            using (SqlConnection sqlcon = new SqlConnection(connectionStr))
            {

                SqlCommand cmd = new SqlCommand(query, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        for(int x = 0; x < tmpColumnCount; x++)
                        {
                            Console.Write($"{dReader.GetName(x)}: {dReader[x]}");
                            Console.WriteLine();
                        }

                        Console.WriteLine();

                        //Console.Write($"");
                        //your code. write stuff to console
                        //Console.WriteLine($"Bear: {dReader["Name"]} \nWeight: {dReader["Weight"]} \nHome: {dReader["CaveId"]} \n \n");

                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }

        }

        public static Student GetStudent()
        {
            string qry = $"SELECT * FROM Student WHERE StudentId = 1";
            Student std = null;
            using (SqlConnection sqlcon = new SqlConnection(connectionStr))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        std = new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());
                        //Console.Write($"");
                        //your code. write stuff to console
                        //Console.WriteLine($"Bear: {dReader["Name"]} \nWeight: {dReader["Weight"]} \nHome: {dReader["CaveId"]} \n \n");

                    }
                    dReader.Close();
                    sqlcon.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

                Console.ReadLine();
            }

            return std;
        }

        public static void GetCourseIds()
        {
            string qry = $"SELECT * FROM Student_Course";

            List<int> courseIds = null;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;


                        //courseIds.Add((int)dReader["StudentId"]);
                        //Console.Write($"");
                        //your code. write stuff to console
                        Console.WriteLine($"Course: {dReader["CourseId"]}");
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    courseIds = null;
                }

            }

        }

        public static bool CheckUserInfo(string username, string password)
        {
            bool infoCorrect = false;

            using (SqlConnection sqlcon = new SqlConnection(connectionStr))
            {

                SqlCommand cmd = new SqlCommand($"LoginInfoCheck", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                cmd.Parameters.Add(new SqlParameter("@password", password));

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
                    infoCorrect = (bool)returnValue.Value;

                }
                catch(Exception ex)
                {

                    Console.WriteLine($"The issue is: {ex.Message}");
                    infoCorrect = false;

                }

            }

                return infoCorrect;
        }
    }
}
