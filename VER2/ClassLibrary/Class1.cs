using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ClassLibrary
{
    public class Class
    {
        public char Letter() // Получение случайной буквы
        {
            Random rnd = new Random();
            int letterNum = rnd.Next(1,33); // Случайный номер буквы по алфавиту
            var assembly = Assembly.GetExecutingAssembly(); // Получение информации о сборке
            string s = "";
            using (Stream stream = assembly.GetManifestResourceStream("ClassLibrary.bin.Debug.alphabet.txt")) // Чтение алфавита из ресурсов программы
            using (StreamReader sr = new StreamReader(stream))
            {
                for (int i = 0; i <= letterNum; i++) // Чтение буквы из алфавита по номеру строки
                {
                    if ((s = sr.ReadLine()) == null)
                    {
                        break;
                    }
                }
            };
            return Convert.ToChar(s); // Метод возвращает загаданную букву
        }
    }
}
