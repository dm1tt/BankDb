using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    
     public class QueryTool:IQueryTool
     {
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5433;User Id=postgres;Password=1234;Database=BankDirectory;");
        }

        private static void InsertUpdateDelete(string insertQuery)
        {
           NpgsqlConnection con = GetConnection();
           NpgsqlCommand insert = new(insertQuery, con);
           con.Open();
           insert.ExecuteNonQuery();
           con.Close();
        }
        
        private static string Select(string selectQuery)
        {
            NpgsqlConnection con = GetConnection();
            NpgsqlCommand select = new(selectQuery, con);
            con.Open();
            string resultOfQuery = select.ExecuteScalar().ToString();
            con.Close();
            return resultOfQuery;
        }

        private static bool SelectExists(string selectExistsQuery)
        {
            NpgsqlConnection con = GetConnection();
            NpgsqlCommand select = new(selectExistsQuery, con);
            con.Open();
            bool resultOfQuery = (bool)select.ExecuteScalar();
            con.Close();
            return resultOfQuery;
        }

        static void GeneralInsertQuery(string fullName, string telephone, string city, string country, string requisites)
        {
            if (SelectExists(@"select exists (select * from ""Residence"" where ""City"" = '"+city+"' )"))
            {
                InsertUpdateDelete(@"insert into ""Client""(""FullName"", ""CityId"", ""Requisites"") values ('" + fullName + "', '" + Select(@"select ""CityId"" from ""Residence"" where ""City"" = '" + city + "';") + "', '" + requisites + "')");
            }
            else
            {
                InsertUpdateDelete(@"insert into ""Residence"" (""City"", ""Country"") values ('" + city + "', '" + country + "')");

                InsertUpdateDelete(@"insert into ""Client""(""FullName"", ""CityId"", ""Requisites"") values ('" + fullName + "', '" + Select(@"select ""CityId"" from ""Residence"" where ""City"" = '" + city + "';") + "', '" + requisites + "')");
            } 
            
            InsertUpdateDelete(@"insert into telephone (""UserId"", ""Phone"") values ('" + Select(@"select ""UserId"" from ""Client"" where ""FullName"" = '" + fullName + "'") + "', '" + telephone + "')");  
        }

        //public void GeneralDeleteQuery(string fullName, string telephone, string city, string country, string requisites)
        //{
        //    if (SelectExists())
        //    {

        //    }
        //}
    }
}
