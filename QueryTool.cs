using Npgsql;

namespace Bank;

public class QueryTool : IQueryTool
{
    private readonly NpgsqlConnection con;
    private List<string> dbQueryResidence;

    public QueryTool()
    {
        con = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=directory;");
        dbQueryResidence = new List<string>();
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
            while (reader.Read())
            {
                dbQueryResidence.AddRange(new[] { reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2) });
            }
        }
        reader.Close();
        con.Close();
    }
    private int ReturnId(string query)
    {
        NpgsqlCommand test = new(query, con);
        con.Open();
        int result = Convert.ToInt32(test.ExecuteScalar());
        con.Close();
        return result;
    }
    public int InsertQuery(string fullName, string phone, string requisites, string city, string country)
    {
        int userId;
        if (!dbQueryResidence.Contains(city))
        {

            dbQueryResidence.AddRange(new[] { (Convert.ToInt32(dbQueryResidence[dbQueryResidence.Count - 3]) + 1).ToString(), city, country });

            SqlQuery($"insert into residence (city, country) values ('{city}','{country}')");
            userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{Convert.ToInt32(dbQueryResidence[dbQueryResidence.Count - 3])}', '{requisites}') returning user_id");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
        else
        {
            userId = ReturnId($"insert into client (fullName, city_id, requisites) values ('{fullName}', '{dbQueryResidence[dbQueryResidence.IndexOf(city) - 1]}', '{requisites}') returning user_id");
            SqlQuery($"insert into telephone (user_id, phone) values ('{userId}', '{phone}')");
        }
        return userId;
    }
    public void DeleteQuery(int userId)
    {
        SqlQuery($"delete from client where user_id = '{userId}'");
    }
    public void GlobalSelectQuery()
    {
        NpgsqlCommand command = new("select client.fullname, telephone.phone, residence.city, residence.country, client.requisites from client join telephone on client.user_id = telephone.user_id join residence on client.city_id = residence.city_id;", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine("{0, 0}  {1, 10}  {2, 10}  {3, 10}  {4, 10}", reader[0], reader[1], reader[2], reader[3], reader[4]);
            }
        }
        reader.Close();
        con.Close();
    }
    public void SingleSelectQuery(int userId)
    {
        NpgsqlCommand command = new($"select client.fullname, telephone.phone, residence.city, residence.country, client.requisites from client join telephone on client.user_id = telephone.user_id join residence on client.city_id = residence.city_id where client.user_id = {userId}", con);
        con.Open();
        NpgsqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            Console.WriteLine("Пользователь с таким ID не найден");
        }
        else
        {
            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{}", reader[0], reader[1], reader[2], reader[3], reader[4]);
            }
        }
        con.Close();
    }
    public void UpdateQuery(int userId, int userAnswer, string newUserData)
    {
        if (userAnswer == 1)
        {
            SqlQuery($"update client set fullname = '{newUserData}' where user_id = {userId}");
        }
        else if (userAnswer == 2)
        {
            SqlQuery($"update telephone set phone = '{newUserData}' where user_id = {userId}");
        }
        else if (userAnswer == 4)
        {
            SqlQuery($"update client set requisites = '{newUserData}' where user_id = {userId}");
        }
    }
    public void UpdateQuery(int userId, string newCity, string newCountry)
    {

        if (!dbQueryResidence.Contains(newCity))
        {
            dbQueryResidence.AddRange(new[] { (Convert.ToInt32(dbQueryResidence[dbQueryResidence.Count - 3]) + 1).ToString(), newCity, newCountry });

            SqlQuery($"update client set city_id = '{Convert.ToInt32(dbQueryResidence[dbQueryResidence.Count - 3])}' where user_id = {userId}");
        }
        else
        {
            SqlQuery($"update client set city_id = '{dbQueryResidence[dbQueryResidence.IndexOf(newCity) - 1]}'");
        }

    }

}
