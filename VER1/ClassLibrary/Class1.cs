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
        public char Letter()
        {
            Random rnd = new Random();
            int letterNum = rnd.Next(1,33);
            var assembly = Assembly.GetExecutingAssembly();
            string s = "";
            using (Stream stream = assembly.GetManifestResourceStream("ClassLibrary.bin.Debug.alphabet.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                for (int i = 0; i <= letterNum; i++)
                {
                    if ((s = sr.ReadLine()) == null)
                    {
                        break;
                    }
                }
            };
            return Convert.ToChar(s);
        }
    }
}
