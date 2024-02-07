using Microsoft.Data.SqlClient;
using System.Data;

namespace B3110SQLInjectionProjectASPNETCore.TechnicalServices
{
    public class Major
    {

        public List<string> GetMajorCodes()
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
