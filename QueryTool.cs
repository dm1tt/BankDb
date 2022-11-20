using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

 public class QueryTool:IQueryTool
 {
    private readonly NpgsqlConnection con;
    private List<string> dbQuery;
    
    public QueryTool()
    {
        con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=directory;");
        dbQuery = new List<string>();
    }

    private void SqlQuery(string insertQuery)
    {
       NpgsqlCommand insert = new(insertQuery, con);
       con.Open();
       insert.ExecuteNonQuery();
       con.Close();
    }
    
    public void Select()
    {
        NpgsqlCommand command = new("select city_id, city, country from residence", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while(reader.Read())
            {
                dbQuery.Add(reader.GetInt32(0).ToString());
                dbQuery.Add(reader.GetString(1));
                dbQuery.Add(reader.GetString(2));
            }
        }
        else
        {
            Console.WriteLine("no rows");
        }
        reader.Close();
        con.Close();
    }

    private string ReturnId(string q)
    {
        NpgsqlCommand test = new(q, con);
        con.Open();
        string result = test.ExecuteScalar().ToString();
        con.Close();
        return result;
    }
    public void InsertQuery(string fullName, string phone, string requisites, string city, string country)
    {
        if(!dbQuery.Contains(city))
        {
            int a = Convert.ToInt32(dbQuery[dbQuery.Count - 3]) + 1;
            dbQuery.Add(a.ToString());
            dbQuery.Add(city);
            dbQuery.Add(country);
            SqlQuery($"insert into residence (city, country) values ('{city}','{country}')");
            string userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{Convert.ToInt32(dbQuery[dbQuery.Count - 3])}', '{requisites}') returning user_id");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
        else
        {
            string userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{Convert.ToInt32(dbQuery[dbQuery.IndexOf(city) - 1])}', '{requisites}')");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
    }

    public void DeleteQuery(int userId)
    {
        SqlQuery($"delete from client where user_id = '{userId}'");
    }

}
