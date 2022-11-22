using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

public class Communication
{
    private readonly IQueryTool user;
    public Communication()
    {
        user = new QueryTool();
    }
    private void StartMenu()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Добавить нового пользователя");
        Console.WriteLine("2. Удалить пользователя");
        Console.WriteLine("3. Обновить данные пользователя");
        Console.WriteLine("4. Показать всех пользователей");
        Console.WriteLine("5. Показать одного пользоватля");
        Console.WriteLine("Для выхода из программы нажмите ESC");
    } 

    private void InsertMenu()
    {
        Console.WriteLine("Введите ФИО");
        string fullName = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Введите номер телефона");
        string phone = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Введите реквизиты");
        string requisites = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Введите город проживания");
        string city = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Введите страну проживания");
        string country = Console.ReadLine();
        Console.WriteLine();

        user.ListCompletion();
        string userId = user.InsertQuery(fullName, phone, requisites, city, country);

        Console.WriteLine("Пользователь успешно добавлен!");
        Console.WriteLine($"Id добавленного пользователя: {userId}");

    }

    private void DeleteMenu()
    {
        Console.WriteLine("Введите ID пользователя, которого вы хотите удалить");
        int userId = Convert.ToInt32(Console.ReadLine());
        user.DeleteQuery(userId);
        Console.WriteLine();
        Console.WriteLine("Пользователь успешно удален!");
    }
    private void SelecUserMenu()
    {
        Console.WriteLine("Введите ID пользователя");
        int userId = Convert.ToInt32(Console.ReadLine());
        user.SingleSelectQuery(userId);
        Console.WriteLine();
    }
    public void Dialog()
    {
        ConsoleKeyInfo escape = Console.ReadKey();
        Console.ReadLine();
        while (escape.Key != ConsoleKey.Escape)
        {
            StartMenu();
            Console.WriteLine();
            escape = Console.ReadKey();
            Console.WriteLine();

            if(escape.Key == ConsoleKey.D1)
            {
                Console.WriteLine();
                InsertMenu();
            }
            else if(escape.Key == ConsoleKey.D2)
            {
                Console.WriteLine();
                DeleteMenu();
            }
            else if(escape.Key == ConsoleKey.D3)
            {
                
            }
            else if (escape.Key == ConsoleKey.D4)
            {
                Console.WriteLine();
                user.GlobalSelectQuery();
            }
            else if(escape.Key == ConsoleKey.D5)
            {
                Console.WriteLine();
                SelecUserMenu();
            }
        }
    }
}
