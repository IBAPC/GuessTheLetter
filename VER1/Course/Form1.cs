using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using ClassLibrary;

namespace Course
{
    public partial class Form1 : Form
    {
        public char letter;

        public Form1()
        {
            string dll = Application.StartupPath + "\\ClassLibrary.dll";
            File.WriteAllBytes(dll, Course.Properties.Resources.ClassLibrary); //Выгрузить dll из ресурсов
            string txt = Application.StartupPath + "\\dictionary.txt";
            File.WriteAllText(txt, Course.Properties.Resources.dictionary); //Выгрузить txt из ресурсов
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "";
                StreamReader sr = new StreamReader("dictionary.txt", Encoding.UTF8);
                s = sr.ReadToEnd();
                sr.Close();
                string FirstWord = s.Split()[0];
                string LastWord = s.Substring(s.LastIndexOf("\n")+1);
                if (s.IndexOf("\n" + textBox1.Text + "\r\n") > -1 || textBox1.Text == FirstWord || textBox1.Text == LastWord)
                {
                        bool exists = false;
                        for (int j = 0; j < textBox1.Text.Length; j++)
                        {
                            s = textBox1.Text;
                            s.ToCharArray();
                            if (letter == s[j])
                            {
                                exists = true;
                            }
                        }
                        if (exists == true)
                        {
                            label2.Text = "В слове ЕСТЬ загаданная буква";
                        }
                        else
                        {
                            label2.Text = "В слове НЕТУ загаданной буквы";
                        }
                }
                else
                {
                    label2.Text = "Слово отсутствует в словаре";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc.Message);
            }
        }

        private void загадатьНовуюБуквуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class rnd = new Class();
            letter = rnd.Letter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class rnd = new Class();
            if (textBox2.Text == letter.ToString())
            {
                MessageBox.Show("Поздравляем вы угадали букву!");
                letter = rnd.Letter();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("К сожалению вы не угадали букву, буква которая была загадана: " + letter);
                letter = rnd.Letter();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.Match(e.KeyChar.ToString(), @"[а-яА-ЯёЁ-]").Success)
            {
                e.Handled = true;
            }
            if (textBox2.Text.Length >= 1)
            {
                e.Handled = true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class rnd = new Class();
            letter = rnd.Letter();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }
    }
}
