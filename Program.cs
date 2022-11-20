using Npgsql;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace Bank;

internal class Program
{
 

    private static void Main(string[] args)
    {
        int userId = Convert.ToInt32(Console.ReadLine());

        IQueryTool user = new QueryTool();
        user.DeleteQuery(userId);
    }       

}