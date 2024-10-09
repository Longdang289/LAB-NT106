﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab2_bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFile.Click += openFile_Click;
        }

        OpenFileDialog opd;
        private void openFile_Click(object sender, EventArgs e)
        {
            try
            {
                opd = new OpenFileDialog();
                opd.Filter = "Text Files |*.txt";
                opd.ShowDialog();
                FileStream fs = new FileStream(opd.FileName, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                input.Text = sr.ReadToEnd();
                sr.BaseStream.Position = 0;
                inputPath.Text = fs.Name;
                calculate.Enabled = true;
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void calculate_Click(object sender, EventArgs e)
        {
            File.WriteAllText("..\\output3.txt", "");
            FileStream fsw = new FileStream("..\\output3.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fsw, Encoding.UTF8);

            FileStream fsr = new FileStream(opd.FileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsr, Encoding.UTF8);

            string[] res = Calc(sr);
            foreach (var item in res)
            {
                sw.WriteLine(item);
            }
            sw.Close();
            output.Text = File.ReadAllText(fsw.Name);
            outputPath.Text = fsw.Name;
            fsw.Close();
            fsr.Close();
        }
        private string[] Calc(StreamReader sr)
        {
            try
            {
                var content = sr.ReadToEnd();

                // Thay thế tất cả các dấu trừ dài (–) bằng dấu trừ ngắn (-)
                content = content.Replace("–", "-");

                var Lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Lines[i].Contains('+') || Lines[i].Contains('-'))
                    {
                        string temp = Lines[i];
                        var calc = Lines[i].Split(new char[] { '-', '+' }); ;
                        float res = 0;
                        for (int j = 0; j < calc.Length; j++)
                        {
                            if (Lines[i].IndexOf('-') != -1 && Lines[i].IndexOf('-') - Lines[i].IndexOf(calc[j]) == -1)
                            {
                                int indexOfSo = Lines[i].IndexOf(calc[j]);
                                int indexOfDau = Lines[i].IndexOf('-');

                                if (indexOfSo != -1)
                                {
                                    int lengthofSo = calc[j].Length;
                                    Lines[i] = Lines[i].Remove(indexOfSo, lengthofSo);
                                }

                                if (indexOfDau != -1 && indexOfDau - (indexOfSo) == -1)
                                {
                                    indexOfSo = Lines[i].IndexOf(calc[j]);
                                    Lines[i] = Lines[i].Remove(indexOfDau, 1);
                                }

                                res -= float.Parse(calc[j]);
                            }
                            else
                            {
                                int indexOfSo = Lines[i].IndexOf(calc[j]);
                                int indexOfDau = Lines[i].IndexOf('+');

                                if (indexOfSo != -1)
                                {
                                    int lengthofSo = calc[j].Length;
                                    Lines[i] = Lines[i].Remove(indexOfSo, lengthofSo);
                                }

                                if (indexOfDau != -1 && indexOfDau - (indexOfSo) == -1)
                                {
                                    indexOfSo = Lines[i].IndexOf(calc[j]);
                                    Lines[i] = Lines[i].Remove(indexOfDau, 1);
                                }

                                res += float.Parse(calc[j]);
                            }
                        }
                        Lines[i] = temp + " = " + res.ToString();
                    }
                    else if (Lines[i].Contains('*'))
                    {
                        var calc = Lines[i].Split('*');
                        float res = 1;
                        for (int j = 0; j < calc.Length; j++)
                        {
                            res *= float.Parse(calc[j]);
                        }
                        Lines[i] = Lines[i] + " = " + res.ToString();
                    }
                    else if (Lines[i].Contains('/'))
                    {
                        var calc = Lines[i].Split('/');
                        float res = 1;
                        for (int j = 0; j < calc.Length; j++)
                        {
                            res /= float.Parse(calc[j]);
                        }
                        Lines[i] = Lines[i] + " = " + res.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Input!");
                    }
                }
                return Lines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new string[] { "Nothing" };
            }
        }


        private void Bai3_Load(object sender, EventArgs e)
        {
            calculate.Enabled = false;
        }
    }
}