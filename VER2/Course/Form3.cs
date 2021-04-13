using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Course
{
    public partial class Form3 : Form
    {
        string path = "dictionary.txt";
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка добавления слова в словарь
        {
            string s = "";
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            s = sr.ReadToEnd();
            sr.Close();
            string FirstWord = s.Split()[0];
            string LastWord = s.Substring(s.LastIndexOf("\n")+1);
            if (s.IndexOf("\n" + textBox1.Text + "\r\n") > -1 || textBox1.Text == FirstWord || textBox1.Text == LastWord) // Проверка наличия слова в словаре
            {
                MessageBox.Show("Слово уже есть в словаре, а потому не может быть добавлено.");
            }
            else // Добавление слова в файла
            {
                File.AppendAllText(path, "\r\n" + textBox1.Text); // Открывает файл добавляет в него текст и закрывает файл (создаёт новый файл в случае его отсутствия)
                label2.Text = "Слово '" + textBox1.Text + "' было успешно добавлено!";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //Контроль ввода только букв кириллицы и "-" в поле ввода слова для добавления/удаления
        {
            if (!Regex.Match(e.KeyChar.ToString(), @"[а-яА-ЯёЁ-]").Success)
            {
                e.Handled = true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка удаления слова из словаря
        {
            string s = "";
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            s = sr.ReadToEnd();
            sr.Close();
            string FirstWord = s.Split()[0];
            string LastWord = s.Substring(s.LastIndexOf("\n")+1);
            if (s.IndexOf("\n" + textBox1.Text + "\r\n") > -1 || textBox1.Text == FirstWord || textBox1.Text == LastWord) // Проверка отсутствия слова в словаре
            {
                File.WriteAllText(path, null);
                StreamWriter sw = new StreamWriter(path);
                int ind = s.IndexOf("\n" + textBox1.Text + "\r\n");
                if (textBox1.Text == LastWord) // Удаление последнего слова в словаре
                {
                    s = s.Remove(s.LastIndexOf("\n") - 1, textBox1.Text.Length + 2);
                    sw.Write(s, Encoding.UTF8);
                    sw.Close();
                }
                else if (textBox1.Text == FirstWord) // Удаление первого слова в словаре
                {
                    s = s.Remove(0, textBox1.Text.Length + 2);
                    sw.Write(s, Encoding.UTF8);
                    sw.Close();
                }
                else // Удаление любого не первого и не последнего слова в словаре
                {
                    s = s.Remove(s.IndexOf("\n" + textBox1.Text + "\r\n"), textBox1.Text.Length+2);
                    sw.Write(s, Encoding.UTF8);
                    sw.Close();
                }
                label2.Text = "Слово '" + textBox1.Text + "' было успешно удалено!";
            }
            else
            {
                MessageBox.Show("Слово отсутствует в словаре, а потому не может быть удалено.");
            }
        }

        private void Form3_Load(object sender, EventArgs e) // Фиксирование размеров формы и заполнение поля со словарём при загрузке формы
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            textBox2.Text = sr.ReadToEnd();
            sr.Close();
        }
    }
}
