using Microsoft.Data.SqlClient;
using System.Data;

namespace B3110SQLInjectionProjectASPNETCoreFinal.TechnicalServices
{
    public class Major
    {

        public List<string> GetMajorCodes()
        {


            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;TrustServerCertificate=true;Integrated Security=True;Database=myTestDB;server=DESKTOP-VT2DNT2\EZRASQLSERVER";
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
