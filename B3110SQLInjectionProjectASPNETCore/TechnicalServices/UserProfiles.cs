using B3110SQLInjectionProjectASPNETCore.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace B3110SQLInjectionProjectASPNETCore.TechnicalServices
{

    public class UserProfiles
    {
        //constructor
        private string? _connectionString;
        public string Static_connectionString = @"Persist Security Info=False;Database=emallo1;server=dev1.baist.ca;User ID=emallo1;Password=Somma16;";
        public UserProfiles()
        {
            //constructor logic
            ConfigurationBuilder DatabaseUserBuilder = new();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            _connectionString = DatabaseUserConfiguration.GetConnectionString("myConnectionString");  //to remove the error'
            //_connectionString = DatabaseUserConfiguration.GetConnectionString("VarBAIS3150")!;  //null forgiving operator  - !
        }




        public UserProfile GetMyLoginDetails(string userName, string password)
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
                CommandText = "SQLIGetLogin"
            };



            //Parameter
            SqlParameter MyParameter;
            MyParameter = new()
            {
                ParameterName = "@Username",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = userName
            };
            MyCommand.Parameters.Add(MyParameter);



            //Parameter
            MyParameter = new()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = password
            };
            MyCommand.Parameters.Add(MyParameter);



            //DataReader
            SqlDataReader MyDataReader;
            MyDataReader = MyCommand.ExecuteReader();
            UserProfile CurrentUserProfile = null;

            if (MyDataReader.HasRows)
            {
                MyDataReader.Read(); // Move to the first row

                CurrentUserProfile = new()
                {
                    Username = (string)MyDataReader["Username"],
                    Password = (string)MyDataReader["Password"],
                    Email = (string)MyDataReader["Email"],
                    Role = (string)MyDataReader["Role"]
                };
                
            }

            return CurrentUserProfile;
        }
    }
}