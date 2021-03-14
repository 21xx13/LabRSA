using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LabRSA
{
    public class RSA
    {
        private static readonly char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                         'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                         'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                         'Э', 'Ю', 'Я', 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и',
                                         'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х',
                                         'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                         '8', '9', '0', '.', ',', '!', '?', '"', '(', ')', ':', ';', '-'};

        public Tuple<MyBigInt, MyBigInt> PublicKey;
        public Tuple<MyBigInt, MyBigInt> PrivateKey;

        private Dictionary<int, MyBigInt> EncodedChars = new Dictionary<int, MyBigInt>();
        private Dictionary<string, int> DecodedChars = new Dictionary<string, int>();
        private List<string> Encode(string s, MyBigInt e, MyBigInt n)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < s.Length; i++)
            {
                var index = (Array.IndexOf(characters, s[i]) == -1 ? 0 : Array.IndexOf(characters, s[i]));
                MyBigInt res;
                if (EncodedChars.ContainsKey(index))
                    res = EncodedChars[index];
                else
                {
                    res = new MyBigInt(index).Pow(e) % n;
                    EncodedChars.Add(index, res);
                }

                result.Add(res.ToString());
            }
            return result;
        }

        private string Decode(List<string> input, MyBigInt d, MyBigInt n)
        {
            var result = new StringBuilder();
            foreach (string item in input)
            {
                long res;
                if (DecodedChars.ContainsKey(item))
                    res = DecodedChars[item];
                else
                {
                    res = MyBigInt.ToLong(new MyBigInt(item).Pow(d) % n);
                    DecodedChars.Add(item, (int)res);
                }

                result.Append(characters[res]);
            }

            return result.ToString();
        }
        private MyBigInt CalculateE(MyBigInt m)
        {
            MyBigInt e = m - 1;
            while (true)
            {
                if (e.IsPrimeNumber() && (e < m) && (MyBigInt.ExtendedEuclid(m, e).Item1 == new MyBigInt(1)))
                    return e;
                e--;
            }
        }

        public void Encrypt(MyBigInt p, MyBigInt q)
        {

            if (p.IsPrimeNumber() && q.IsPrimeNumber())
            {
                var s = new StringBuilder();
                try{
                    StreamReader sr = new StreamReader("in.txt");
                    while (!sr.EndOfStream)
                        s.Append(sr.ReadLine());

                    sr.Close();
                } catch{
                    Console.WriteLine("Файл in.txt не найден");
                    return;
                }
                
                MyBigInt n = p * q;
                MyBigInt phi = (p - 1) * (q - 1);
                MyBigInt e = CalculateE(phi);
                MyBigInt d = (MyBigInt.Inverse(new MyBigInt(e), new MyBigInt(phi)));
                if (d == e)
                    d += phi;
                List<string> result = Encode(s.ToString(), e, n);
                StreamWriter sw = new StreamWriter("out1.txt");
                foreach (string item in result)
                    sw.WriteLine(item);
                sw.Close();

                PublicKey = new Tuple<MyBigInt, MyBigInt>(e, n);
                PrivateKey = new Tuple<MyBigInt, MyBigInt>(d, n);

                Console.WriteLine("Публичный ключ - ({0}, {1})", e, n);
                Console.WriteLine("Приватный ключ - ({0}, {1})", d, n);
                Process.Start("out1.txt");
            }
            else
                Console.WriteLine("p или q - не простые числа!");

        }

        public void Decipher(MyBigInt d, MyBigInt n)
        {
            List<string> input = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("out1.txt");
                while (!sr.EndOfStream)
                    input.Add(sr.ReadLine());
                sr.Close();
            }
            catch {
                Console.WriteLine("Файл out1.txt не найден");
                return;
            }
            string result = Decode(input, d, n);
            StreamWriter sw = new StreamWriter("out2.txt");
            sw.WriteLine(result);
            sw.Close();
            Process.Start("out2.txt");
        }
    }

}
