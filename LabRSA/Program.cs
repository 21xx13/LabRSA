using System;
using System.Diagnostics;

namespace LabRSA
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new RSA();
            Console.WriteLine("Файл с сообщением должен лежать в корневой папке и иметь имя in.txt");
            while (true)
            {
                Console.WriteLine("Если вы хотите зашифровать сообщение, введите 1.\nЕсли вы хотите расшифровать сообщение, введите 2." +
                    "\nЕсли вы хотите посмотреть справочную информацию о программе, введите 3" + "\nЗавершить работу - введите 0");
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    if (input == "1" || input == "2" || input == "0" || input == "3")
                        break;
                    Console.WriteLine("Неверное значение");
                }

                if (input == "1")
                {
                    string a, b;
                    Console.WriteLine("Введите 2 простых числа");
                    a = InputNumber();
                    b = InputNumber();
                    test.Encrypt(new MyBigInt(a), new MyBigInt(b));
                }

                else if (input == "2")
                {
                    Console.WriteLine("Введите приватный ключ");
                    Console.WriteLine("Значение d:");
                    var d = Console.ReadLine();
                    Console.WriteLine("Значение n:");
                    var n = Console.ReadLine();
                    test.Decipher(new MyBigInt(d), new MyBigInt(n));
                }

                else if (input == "0") break;
                else if (input == "3")
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Справка:");
                    Console.WriteLine("\tИсходное сообщение для шифрования должно быть в виде текстового файла. Файл с сообщением должен лежать в папке в той же папке, что и исполняемый файл «LabRSA.exe» и иметь имя in.txt. Сообщение может содержать только буквы русского алфавита (А-Я, а-я), цифры (0-9) и следующие символы ‘#’, ‘.’ (точка), ‘,’ (запятая), ‘!’, ‘?’, ‘(’, ‘)’ (круглые скобки), ‘:’ (двоеточие), ‘;’ (точка с запятой), ‘-’ (дефис)  и двойные кавычки." +
                        "\n\tЧтобы зашифровать сообщение, необходимо ввести 2 простых числа.На их основе сгенерируются публичный и приватный ключи, которые отобразятся в консоли." +
                        "\n\tРезультат шифрования находится в файле out1.txt и лежит в той же папке." +
                        "\n\tЧтобы расшифровать сообщение, необходимо ввести данные приватного ключа(2 числа).Файл, который необходимо расшифровать должен иметь имя out1.txt и находится в той же папке, что и исполняемый файл «LabRSA.exe». " +
                        "\n\tФормат текстовых данных для расшифровки должен быть следующим: в файле могут содержаться только числа, каждое число должно начинаться с новой строки." +
                        "\n\tРасшифрованные данные находятся в файле out2.txt и лежат в той же папке.");
                    Console.WriteLine("--------------------");
                }
            }
        }
        private static string InputNumber()
        {
            string number = "";
            while (true)
            {
                number = Console.ReadLine();
                if (new MyBigInt(number).IsPrimeNumber())
                    break;
                Console.WriteLine("Число не простое");
            }
            return number;
        }
    }

}

