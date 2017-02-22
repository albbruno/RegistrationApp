using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University;
using DatabaseLib;

namespace RegistrationApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string inQuery;
            bool queryInput = false;
            string x;

            Console.WriteLine(ConnectionObj.CheckUserInfo("popy", "poop"));

            ConnectionObj.GetCourseIds();

            //Student test = ConnectionObj.GetStudent();

            //Console.WriteLine($"{test.Name}\n{test.StudentId}");

            /*
            do
            {
                Console.WriteLine("1 to execute query, 0 to quit");
                x = Console.ReadLine();

                if (x == "1")
                {
                    Console.WriteLine("R = Read Query. \nW = Write Query");

                    if (Console.ReadLine().ToString() == "R")
                    {
                        Console.WriteLine("Input a query: ");
                        inQuery = Console.ReadLine().ToString();
                        queryInput = true;

                        if (queryInput)
                        {
                            ConnectionObj.ShowReadResults(inQuery);
                            Console.WriteLine();
                            queryInput = false;
                        }
                    }
                    else;


                }
            }
            while (x == "1");
            */

            Console.ReadLine().ToString();
        }

        //static void GetStudent(out Student test, int id)
        //{
        //    string qry = $"SELECT * FROM Student WHERE StudentId = {id}";
        //    test = new Student();
        //
        //}

        /*
        public static void ShowReadResults(string query)
        {

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

                        for (int x = 0; x < tmpColumnCount; x++)
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }

        }*/

    }
}
