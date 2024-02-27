using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace B3110SQLInjectionProjectASPNETCore.TechnicalServices
{
    public class Majors
    {

        

        //constructor
        private string? _connectionString;
        public string Static_connectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Somma16;";
        public Majors()
        {
            //constructor logic
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("myConnectionString");  //to remove the error'
            //_connectionString = DatabaseUserConfiguration.GetConnectionString("VarBAIS3150")!;  //null forgiving operator  - !
        }



        public List<string> GetMajorCodes()
        {


            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = Static_connectionString;
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SQLIGetMajorCodes"
            };

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();


            List<string> MajorCodeDDList = new();
            string MyMajorCode = string.Empty;

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {

                    MyMajorCode = (string)MyDataReader["MajorCode"];
                    MajorCodeDDList.Add(MyMajorCode);
                }
            }
            return MajorCodeDDList;

        }

    }
}
