using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank;

public class Choice
{
    public int choice()
    {
        int answer = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Выберите вариант ответа:");
        Console.WriteLine("1. КОНЕЧНО ЖЕ ДА!!  2.НЕТ Я ЧМО((");
        return answer;
    }
   public void dialog() {

        Console.WriteLine("Здарова мужик! Предлагаю тебе взять кредит в нашем прекраснм и мега популярном БДСМ банке!!");
        Console.ReadKey();
        Console.WriteLine("Полученные деньги ты впарве тратить на абсолютно любые приколы штуки для секса и всякие разные сладости из пятерочки!!!");
        Console.ReadKey();
        Console.WriteLine("Ну что? Приступаем к оформлению кредита?");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine("Выберите вариант ответа:");
        Console.WriteLine();
        Console.WriteLine("1. КОНЕЧНО ЖЕ ДА!!  2.НЕТ Я ЧМО((");
        while (choice()==2)
        {
            choice();
        }
   }

   
}
