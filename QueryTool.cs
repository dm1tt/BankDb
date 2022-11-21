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
    public void ListCompletion()
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
        reader.Close();
        con.Close();
       
    }
    private string ReturnId(string query)
    {
        NpgsqlCommand test = new(query, con);
        con.Open();
        string result = test.ExecuteScalar().ToString();
        con.Close();
        return result;
    }
    public string InsertQuery(string fullName, string phone, string requisites, string city, string country)
    {
        string userId;
        if(!dbQuery.Contains(city))
        {

            dbQuery.Add((Convert.ToInt32(dbQuery[dbQuery.Count - 3]) + 1).ToString());
            dbQuery.Add(city);
            dbQuery.Add(country);            

            SqlQuery($"insert into residence (city, country) values ('{city}','{country}')");
            userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{Convert.ToInt32(dbQuery[dbQuery.Count - 3])}', '{requisites}') returning user_id");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
        else
        {
            userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{Convert.ToInt32(dbQuery[dbQuery.IndexOf(city) - 1])}', '{requisites}')");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
        return userId;
    }
    public void DeleteQuery(int userId)
    {
        SqlQuery($"delete from client where user_id = '{userId}'");
    }
    public void SelectQuery()
    {
        
    }
}
