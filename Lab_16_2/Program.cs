using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

namespace Lab_16_3
{
    class Program
    {
        public static object JsonConvert { get; private set; }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            const int n = 5;
            Product[] product = new Product[n];
            product[0] = new Product(1, "Товар1", 1);
            product[1] = new Product(2, "Товар2", 2);
            product[2] = new Product(3, "Товар3", 3);
            product[3] = new Product(4, "Товар4", 4);
            product[4] = new Product(5, "Товар5", 5);
            foreach (var i in product)
            {
                i.Info();
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(product, options);
            string path = "D:\\Lena\\BIM\\Lab_16_1\\Product.json";
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(jsonString);
            }
            StreamReader sr = new StreamReader(path);
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
            Product[] product1 = JsonSerializer.Deserialize<Product[]>(jsonString, options);
            char[] delimiterChars = { ' ', '{', '}', ':', '\t', '\n', ',', '[', ']', '\v', '"', '"', '\b', '\f', '\r' };
            string[] words = jsonString.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                Console.WriteLine($"<{word}>");
            }
            int max = 0;
            string name = " ";
            for (int j = 0; j < n; j++)
            {
                int a = Int32.Parse(words[n + j * 6]);
                if (a > max)
                {
                    max = a;
                    name = words[n + j * 6 - 2];
                }
            }
            Console.WriteLine("Самый дорогой товар {0} с ценой {1}", name, max);
            Console.ReadKey();
        }
    }
    public class Product
    {
        public int code;
        public string name;
        public double price;
        public int Code { get; set; }
        public string Name { get; set; }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    Console.WriteLine("Цена должна быть больше нуля");
                }
            }
        }
        public Product()
        {

        }
        public Product(int code, string name, double price)
        {
            Code = code;
            Name = name;
            Price = price;
        }
        public void Info()
        {
            try
            {
                Console.Write("введите артикул {0} ", Code);
                Code = Convert.ToInt32(Console.ReadLine());
                Console.Write("введите название {0} ", Name);
                Name = Convert.ToString(Console.ReadLine());
                Console.Write("введите цену {0} ", Price);
                Price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("Ошибка! Входная строка имела неверный формат");
            }
        }
    }
}
