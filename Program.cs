using Npgsql;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestConection();
            Console.ReadKey();
        }
        private static void TestConection()
        {
            using(NpgsqlConnection con = GetConnection())
            {
                con.Open();
                if(con.State== ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                }
            }
        }
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5433;User Id=postgres;Password=1234;Database=BankDirectory;");
        }
    }
}