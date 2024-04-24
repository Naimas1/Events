using System;
using System.IO;
using System.Threading;

class Program
{
    static readonly object locker = new object();
    static readonly Random random = new Random();

    static void Main()
    {
        // Запускаємо перший потік для генерації та збереження пар чисел у файл
        Thread t1 = new Thread(GenerateNumbersToFile);
        t1.Start();

        // Запускаємо другий потік для підрахунку суми кожної пари чисел
        Thread t2 = new Thread(CalculateSum);
        t2.Start();

        // Запускаємо третій потік для підрахунку добутку кожної пари чисел
        Thread t3 = new Thread(CalculateProduct);
        t3.Start();

        // Очікуємо завершення всіх потоків
        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine("Готово!");
    }

    static void GenerateNumbersToFile()
    {
        // Генеруємо 10 пар чисел та зберігаємо їх у файл
        using (StreamWriter sw = new StreamWriter("numbers.txt"))
        {
            for (int i = 0; i < 10; i++)
            {
                int num1 = random.Next(1, 11);
                int num2 = random.Next(1, 11);
                sw.WriteLine($"{num1},{num2}");
            }
        }
    }

    static void CalculateSum()
    {
        string[] lines = File.ReadAllLines("numbers.txt");
        using (StreamWriter sw = new StreamWriter("sums.txt"))
        {
            foreach (string line in lines)
            {
                string[] numbers = line.Split(',');
                int num1 = int.Parse(numbers[0]);
                int num2 = int.Parse(numbers[1]);
                int sum = num1 + num2;
                sw.WriteLine(sum);
            }
        }
    }

    static void CalculateProduct()
    {
        string[] lines = File.ReadAllLines("numbers.txt");
        using (StreamWriter sw = new StreamWriter("products.txt"))
        {
            foreach (string line in lines)
            {
                string[] numbers = line.Split(',');
                int num1 = int.Parse(numbers[0]);
                int num2 = int.Parse(numbers[1]);
                int product = num1 * num2;
                sw.WriteLine(product);
            }
        }
    }
}
