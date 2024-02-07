using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Microsoft.Extensions.Logging; // Import the necessary namespace


namespace B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices
{
    public class Students
    {
        
        
        
        public bool AddStudent(Student acceptedStudent, string programCode)
        {
            bool Success = false;
            SqlConnection MyDataSource = new();

            try
            {
                //connection
                MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
                MyDataSource.Open();

                //command
                SqlCommand MyCommand = new()
                {
                    Connection = MyDataSource,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SQLIAddStudent"
                };

                //Parameter
                SqlParameter MyParameter;
                MyParameter = new()
                {
                    ParameterName = "@StudentID",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.StudentID
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.FirstName
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.LastName
                };
                MyCommand.Parameters.Add(MyParameter);


                MyParameter = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.Email
                };
                MyCommand.Parameters.Add(MyParameter);


                MyParameter = new()
                {
                    ParameterName = "@ProgramCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.ProgramCode
                };
                MyCommand.Parameters.Add(MyParameter);




                MyParameter = new()
                {
                    ParameterName = "@Term",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.Term
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@MajorCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = acceptedStudent.MajorCode
                };
                MyCommand.Parameters.Add(MyParameter);

                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                Success = true;
            }
            catch (SqlException sqlException)
            {
                // Log SQL exceptions
                //Console.WriteLine($"SQL Exception: {sqlException.Message}");

                // Check if it's a duplicate key violation
                //if (sqlException.Number == 2627 || sqlException.Number == 2601)
                //{
                //    // Handle duplicate key violation
                //    //Console.WriteLine("Duplicate key violation detected.");
                //}

                // Propagate the exception back to the calling code
                throw;
            }
            catch (Exception ex)
            {
                // Log general exceptions
                //Console.WriteLine($"Exception: {ex.Message}");

                // Propagate the exception back to the calling code
                throw;
            }
            return Success;
        
        }


        public Student GetStudent(string studentID)
        {
            //Connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";

            MyDataSource.Open();

            //command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetStudent"
            };

            //Parameter
            SqlParameter MyParameter;
            MyParameter = new()
            {
                ParameterName = "@StudentID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = studentID
            };
            MyCommand.Parameters.Add(MyParameter);


            //DataReader
            SqlDataReader MyDataReader;
            MyDataReader = MyCommand.ExecuteReader();
            Student Enrolledstudent = null;

            if (MyDataReader.HasRows)
            {
                MyDataReader.Read(); // Move to the first row

                Enrolledstudent = new()
                {
                    StudentID = MyDataReader["StudentID"].ToString(),
                    FirstName = MyDataReader["FirstName"].ToString(),
                    LastName = MyDataReader["LastName"].ToString(),
                    Email = MyDataReader["Email"].ToString(),
                    ProgramCode = MyDataReader["ProgramCode"].ToString(),
                    Term = MyDataReader["Term"].ToString(),
                    MajorCode = MyDataReader["MajorCode"].ToString()
                };
            };
            MyDataReader.Close();
            MyDataSource.Close();
            return Enrolledstudent;

        }



        public List<Student> GetStudents(string programCode)
        {
            //Conneciton
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetStudentByProgram"
            };

            //Paratmeter
            SqlParameter MyParameter = new()
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = programCode
            };
            MyCommand.Parameters.Add(MyParameter);

            //DataReader
            SqlDataReader MyDataReader = MyCommand.ExecuteReader();
            List<Student> EnrolledStudents = new();


            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    Student EnrolledStudent = new()
                    {
                        StudentID = (string)MyDataReader["StudentID"],
                        FirstName = (string)MyDataReader["FirstName"],
                        LastName = (string)MyDataReader["LastName"],
                        Email = (string)MyDataReader["Email"],
                        ProgramCode = MyDataReader["ProgramCode"].ToString(),
                        Term = MyDataReader["Term"].ToString(),
                        MajorCode = MyDataReader["MajorCode"].ToString()
                    };
                    EnrolledStudents.Add(EnrolledStudent);
                }
            }


            MyDataReader.Close();
            MyDataSource.Close();

            return EnrolledStudents;
        }


        public bool DeleteStudent(string studentID)
        {
            bool Success = false;


            //sqlconnection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();


            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIDeleteStudent"
            };

            //Parameter
            SqlParameter MyParameter = new()
            {
                ParameterName = "@StudentID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = studentID
            };

            MyCommand.Parameters.Add(MyParameter);
            MyCommand.ExecuteNonQuery();
            MyDataSource.Close();
            Success = true;
            return Success;
        }
        public bool UpdateStudent(Student enrolledStudent)
        {
            bool Success;
            Success = false;

            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();


            //command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIUpdateStudent"
            };

            //Parameter
            SqlParameter MyParameter;
            MyParameter = new()
            {
                ParameterName = "@StudentID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.StudentID
            };
            MyCommand.Parameters.Add(MyParameter);

            MyParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.FirstName
            };
            MyCommand.Parameters.Add(MyParameter);


            MyParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.LastName
            };
            MyCommand.Parameters.Add(MyParameter);


            MyParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.Email
            };
            MyCommand.Parameters.Add(MyParameter);

            MyParameter = new()
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.ProgramCode
            };
            MyCommand.Parameters.Add(MyParameter);

            MyParameter = new()
            {
                ParameterName = "@Term",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.Term
            };
            MyCommand.Parameters.Add(MyParameter);

            MyParameter = new()
            {
                ParameterName = "@MajorCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = enrolledStudent.MajorCode
            };
            MyCommand.Parameters.Add(MyParameter);
            
            
            MyCommand.ExecuteNonQuery();
            MyDataSource.Close();
            Success = true;
            return Success;
        }


    }
}
