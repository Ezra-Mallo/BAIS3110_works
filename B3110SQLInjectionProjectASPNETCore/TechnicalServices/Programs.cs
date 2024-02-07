using B3110SQLInjectionProjectASPNETCore.Domain;
using System.Data;
using Microsoft.Data.SqlClient;

namespace B3110SQLInjectionProjectASPNETCore.TechnicalServices
{
    public class Programs
    {


        public List<Domain.Program> GetPrograms()
        {


            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Dalu21";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetPrograms"
            };
            SqlDataReader MyDataReader = MyCommand.ExecuteReader();


            List<Domain.Program> allPrograms = new();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    Domain.Program aProgram = new();
                    aProgram.ProgramCode = (string)MyDataReader["ProgramCode"];
                    aProgram.Description = (string)MyDataReader["Description"];
                    allPrograms.Add(aProgram);
                }
                
            }
            return allPrograms;
        }







        public bool AddProgram(string programCode, string description)
        {
            bool Success = false;

            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Dalu21";
            MyDataSource.Open();

            //command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIAddProgram"
            };

            //Parameter
            SqlParameter MyParameter;
            MyParameter = new()
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = programCode
            };
            MyCommand.Parameters.Add(MyParameter);

            MyParameter = new()
            {
                ParameterName = "@Description",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = description
            };
            MyCommand.Parameters.Add(MyParameter);


            MyCommand.ExecuteNonQuery();

            MyDataSource.Close();
            Success = true;
            return Success;

        }
        public Domain.Program GetProgram(string programCode)
        {

            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Dalu21";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetProgram"
            };

            //Parameter
            SqlParameter MyParameter = new()
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = programCode
            };

            MyCommand.Parameters.Add(MyParameter);



            Domain.Program ActiveProgram = new();
            ActiveProgram.ProgramCode = programCode;
            ActiveProgram.Description = (string)MyCommand.ExecuteScalar();

            List<Student> EnrolledStudents;

            Students StudentManager = new();
            EnrolledStudents = StudentManager.GetStudents(programCode);
            ActiveProgram.EnrolledStudents.AddRange(EnrolledStudents);

            return ActiveProgram;
        }


        public List<string> GetProgramCodes()
        {


            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Dalu21";
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetProgramCodes"
            };

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();


            List<string> ProgramCodeDDList = new();
            string MyProgramCode = string.Empty;

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {

                    MyProgramCode = (string)MyDataReader["ProgramCode"];
                    ProgramCodeDDList.Add(MyProgramCode);
                }
            }
            return ProgramCodeDDList;

        }
    }

}