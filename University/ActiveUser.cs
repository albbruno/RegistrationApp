using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class ActiveUser
    {
        public string Username;
        public string Password;
        public int userid;
        public int studentId;
        public List<int> courseIds;
        public Student Student;

        public static ActiveUser GetInstance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ActiveUser(); 
                }
                return _Instance;
            }
        }

        private static ActiveUser _Instance;

        private ActiveUser()
        {
        }

        private List<int> GetCourseIds(int sId)
        {
            string qry = $"SELECT * FROM Student";// WHERE StudentId = {sId}";

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

                        courseIds.Add((int)dReader["CourseId"]);
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
                    courseIds = null;
                }

            }

            return courseIds;

        }

        public void Clear()
        {
            Username = "";
            Password = "";
            userid = 0;
            studentId = 0;
            courseIds = null;
            Student = null;
        }
    }
}
