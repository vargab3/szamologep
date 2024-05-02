using System;
using System.IO;
using System.Linq; // A LINQ névtér importálása az Enumerable.Sum() használatához

class Calculator
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Kérlek válassz egy műveletet:");
            Console.WriteLine("1. Összeadás");
            Console.WriteLine("2. Kivonás");
            Console.WriteLine("3. Szorzás");
            Console.WriteLine("4. Osztás");
            Console.WriteLine("5. Eredmények megtekintése");
            Console.WriteLine("6. Kilépés");
            Console.Write("Választott művelet: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    PerformOperation("összeadás");
                    break;
                case "2":
                    PerformOperation("kivonás");
                    break;
                case "3":
                    PerformOperation("szorzás");
                    break;
                case "4":
                    PerformOperation("osztás");
                    break;
                case "5":
                    DisplayResults();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás. Kérlek válassz újra.");
                    break;
            }
        }
    }

    static void PerformOperation(string operationName)
    {
        Console.Write($"Kérem, hány számot szeretne megadni az {operationName} művelethez: ");
        if (!int.TryParse(Console.ReadLine(), out int count) || count < 2)
        {
            Console.WriteLine("Érvénytelen bemenet. Minimum 2 számot kell megadni.");
            return;
        }

        double[] numbers = new double[count];
        for (int i = 0; i < count; i++)
        {
            Console.Write($"Kérem adja meg a(z) {i + 1}. számot: ");
            if (!double.TryParse(Console.ReadLine(), out numbers[i]))
            {
                Console.WriteLine("Érvénytelen bemenet. Egy valós számot kell megadni.");
                return;
            }
        }

        double result = Calculate(operationName, numbers);
        Console.WriteLine($"Eredmény: {result}");

        // Eredmény mentése txt fájlba
        string resultLine = $"{operationName} (";
        for (int i = 0; i < numbers.Length; i++)
        {
            resultLine += numbers[i];
            if (i < numbers.Length - 1)
                resultLine += ", ";
        }
        resultLine += $") = {result}";
        SaveResultToFile(resultLine);
    }

    static double Calculate(string operationName, double[] numbers)
    {
        switch (operationName)
        {
            case "összeadás":
                return numbers.Sum(); // Az Enumerable.Sum() használata
            case "kivonás":
                double subtractResult = numbers[0];
                for (int i = 1; i < numbers.Length; i++)
                {
                    subtractResult -= numbers[i];
                }
                return subtractResult;
            case "szorzás":
                double multiplyResult = 1;
                foreach (var number in numbers)
                {
                    multiplyResult *= number;
                }
                return multiplyResult;
            case "osztás":
                double divideResult = numbers[0];
                for (int i = 1; i < numbers.Length; i++)
                {
                    if (numbers[i] == 0)
                    {
                        Console.WriteLine("Nullával való osztás nem megengedett.");
                        return double.NaN; // visszatérés NaN (Not-a-Number) értékkel
                    }
                    divideResult /= numbers[i];
                }
                return divideResult;
            default:
                return 0;
        }
    }

    static void SaveResultToFile(string result)
    {
        string filePath = "calculator_results.txt";
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba a fájl mentése közben: {ex.Message}");
        }
    }

    static void DisplayResults()
    {
        string filePath = "calculator_results.txt";
        try
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("Mentett eredmények:");
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("Nincsenek mentett eredmények.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba a fájl olvasása közben: {ex.Message}");
        }
    }
}
