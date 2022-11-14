using Npgsql;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace Bank
{
    internal class Program
    {
        private static int choice()
        {
            Console.WriteLine("Выберите вариант ответа:");
            Console.WriteLine("1. КОНЕЧНО ЖЕ ДА!!  2.НЕТ Я ЧМО((");
            return Convert.ToInt32(Console.ReadLine());
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Здарова мужик! Предлагаю тебе взять кредит в нашем прекраснм и мега популярном БДСМ банке!!");
            Console.ReadKey();
            Console.WriteLine("Полученные деньги ты впарве тратить на абсолютно любые приколы штуки для секса и всякие разные сладости из пятерочки!!!");
            Console.ReadKey();
            Console.WriteLine("Ну что? Приступаем к оформлению кредита?");
            Console.WriteLine();
            Console.ReadKey();

            while (choice() == 2) 
            {
                choice();
            }            

            string fullName = Console.ReadLine();
            string phone = Console.ReadLine();
            string city = Console.ReadLine();
            string country = Console.ReadLine();
            string reqs = Console.ReadLine(); 
            QueryTool user = new QueryTool();
            user.GeneralInsertQuery(fullName, phone, city, country, reqs);
            Console.WriteLine(")))");
            Console.ReadKey();
        }       

    }
}