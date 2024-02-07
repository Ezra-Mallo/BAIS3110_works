using B3110SQLInjectionProjectASPNETCoreFinal.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Formats.Asn1.AsnWriter;

namespace B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices
{
    public class ExamScores
    {
        public bool RegisterExamScore(ExamScore registerCredit)
        {

            bool success = false;
            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIRegisterCourseAndScore"
            };



            //Parameter
            SqlParameter MyParameter;
            MyParameter = new()
            {
                ParameterName = "@StudentID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.StudentID
            };
            MyCommand.Parameters.Add(MyParameter);


            //Parameter
            MyParameter = new()
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.ProgramCode
            };
            MyCommand.Parameters.Add(MyParameter);


            //Parameter
            MyParameter = new()
            {
                ParameterName = "@CourseCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.CourseCode
            };
            MyCommand.Parameters.Add(MyParameter);

            //Parameter
            MyParameter = new()
            {
                ParameterName = "@MajorCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.MajorCode
            };
            MyCommand.Parameters.Add(MyParameter);

            //Parameter
            MyParameter = new()
            {
                ParameterName = "@Term",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.Term
            };
            MyCommand.Parameters.Add(MyParameter);

            //Parameter
            MyParameter = new()
            {
                ParameterName = "@Semester",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.Semester
            };
            MyCommand.Parameters.Add(MyParameter);

            //Parameter
            MyParameter = new()
            {
                ParameterName = "@Scores",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = registerCredit.Scores
            };
            MyCommand.Parameters.Add(MyParameter);

            MyCommand.ExecuteNonQuery();
            MyDataSource.Close();
            success = true;
            return success;
        }



        public List<ExamScore> CheckGradeParametizedStoredProcedure(string studentID, string term)
        {

            List<ExamScore> examScores = new();
            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetExamScore"
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


            //Parameter
            MyParameter = new()
            {
                ParameterName = "@Term",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = term
            };
            MyCommand.Parameters.Add(MyParameter);

            SqlDataReader MyDataReader;
            MyDataReader = MyCommand.ExecuteReader();

            if (MyDataReader.HasRows)
            {
                ExamScore examScore = new();
                while (MyDataReader.Read())
                {
                    examScore = new()
                    {
                        StudentID = (string)MyDataReader["StudentID"],
                        ProgramCode = (string)MyDataReader["ProgramCode"],
                        CourseCode = (string)MyDataReader["CourseCode"],
                        MajorCode = (string)MyDataReader["MajorCode"],
                        Term = (string)MyDataReader["Term"],
                        Semester = (int)MyDataReader["Semester"],
                        Scores = (int)MyDataReader["Scores"]
                    };
                    examScores.Add(examScore);
                }
            }
            MyDataSource.Close();
            return examScores;
        }

        public List<ExamScore> CheckGradeVulnerableAdapter(string studentID, string term)
        {

            List<ExamScore> examScores = new();
            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.Text,
                CommandText = "SELECT StudentID, ProgramCode, CourseCode, MajorCode, Term, Semester, Scores  FROM SQLIExamScore WHERE term = '" + term + "' AND StudentID = '" + studentID + "'"
            };

            // data adapter
            SqlDataAdapter MyDataAdapter = new SqlDataAdapter(MyCommand);

            // data set
            DataSet MyDataSet = new DataSet();

            // fill data set
            MyDataAdapter.Fill(MyDataSet);


            // Populate ExamScores
            if (MyDataSet.Tables.Count > 0 && MyDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in MyDataSet.Tables[0].Rows)
                {
                    ExamScore examScore = new ExamScore
                    {
                        StudentID = (string)row["StudentID"],
                        ProgramCode = (string)row["ProgramCode"],
                        CourseCode = (string)row["CourseCode"],
                        MajorCode = (string)row["MajorCode"],
                        Term = (string)row["Term"],
                        Semester = (int)row["Semester"],
                        Scores = (int)row["Scores"]
                    };
                    examScores.Add(examScore);
                }
            }




            MyDataSource.Close();
            return examScores;

        } 
    }
}

