namespace Bank;

public class Communication
{
    private readonly IQueryTool user;
    public Communication()
    {
        user = new QueryTool();
        user.ListCompletion();
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
        Console.WriteLine("Введите город");
        string city = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Введите страну");
        string country = Console.ReadLine();
        Console.WriteLine();

        int userId = user.InsertQuery(fullName, phone, requisites, city, country);

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
    private void UpdateMenu()
    {
        Console.WriteLine("Введине ID пользователя");
        int userId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Какие данные вы ходите обновить?");
        Console.WriteLine("1. ФИО");
        Console.WriteLine("2. Номер телефона");
        Console.WriteLine("3. Место проживания");
        Console.WriteLine("4. Реквизиты");
        int userAnswer = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        if(userAnswer == 3)
        {
            Console.WriteLine("Введите город");
            string newCity = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Введите страну");
            string newCountry = Console.ReadLine();
            Console.WriteLine();
            user.UpdateQuery(userId, newCity, newCountry);
        }
        else
        {
            Console.WriteLine("Введите новые данные");
            string newUserData = Console.ReadLine();
            user.UpdateQuery(userId,userAnswer, newUserData);
        }
        Console.WriteLine("Данные успешно обновлены!");

    }
    private void SelectUserMenu()
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
                Console.WriteLine();
                UpdateMenu();
            }
            else if (escape.Key == ConsoleKey.D4)
            {
                Console.WriteLine();
                user.GlobalSelectQuery();
            }
            else if(escape.Key == ConsoleKey.D5)
            {
                Console.WriteLine();
                SelectUserMenu();
            }
        }
    }
}
