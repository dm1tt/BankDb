﻿using Npgsql;
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
        //Choice dialog = new();
        //dialog.dialog();

        //string fullName = Console.ReadLine();
        //string phone = Console.ReadLine();
        //string requisites = Console.ReadLine();
        //string city = Console.ReadLine();
        //string country = Console.ReadLine();
        int userId = Convert.ToInt32(Console.ReadLine());

        IQueryTool user = new QueryTool();
        user.DeleteQuery(userId);
        //user.Select();
        //user.InsertQuery(fullName, phone, requisites, city, country);

    //    Console.WriteLine(")))");
    //    Console.ReadKey();
    }       

}