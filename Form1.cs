using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Variant5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == ""))
            {
                MessageBox.Show("Пожалуйста убедитесь в правильности ввода данных и повторите попытку");
            }
            else
            {
                int x = Convert.ToInt32(textBox3.Text);
                int imax = Convert.ToInt32(textBox4.Text);
                int period = 2; 
                int a = Convert.ToInt32(textBox1.Text); 
                int x0 = 20; 
                int y0 = a;
                double[] f = new double[imax * period + 10];
                double b = Convert.ToDouble(textBox2.Text);
                IStrategy s = new SinA();
                if (comboBox1.Text == "SinA")
                    s = new SinA();
                if (comboBox1.Text == "CosA")
                    s = new CosA();
                if (comboBox1.Text == "TanA")
                    s = new TanA();
                if (comboBox1.Text == "CtanA")
                    s = new CtngA();

                // Функция
                for (int i = 0; i < imax * period; i++)
                {
                    f[i] = s.Function(a, b, i, imax, x);
                }

                Graphics g = Graphics.FromHwnd(this.Handle);
                Pen pen = Pens.Black;

                g.DrawLine(pen, x0, y0, x0 + imax * period, y0); //Рисуем ось X
                g.DrawLine(pen, x0, y0 - a, x0, y0 + a); //Рисуем ось Y

                for (int i = 0; i < imax * period; i++) //Рисуем график
                {
                    int y1 = y0 - (int)f[i]; //Координата Y[i]
                    int y2 = y0 - (int)f[i + 1]; //Координата Y[i+1]
                    g.DrawLine(pen, x0 + i, y1, x0 + i + 1, y2);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        public interface IStrategy
        {
            public double Function(int a, double b, int i, int imax, int x);
        }
        public class SinA : IStrategy
        {
            public double Function(int a, double b, int i, int imax, int x)
            {
                return Math.Round(a * Math.Sin(b * (x * Math.PI / imax * i)));
            }
        }
        public class CosA : IStrategy
        {
            public double Function(int a, double b, int i, int imax, int x)
            {
                return Math.Round(a * Math.Cos(b * (x * Math.PI / imax * i)));
            }
        }
        public class TanA : IStrategy
        {
            public double Function(int a, double b, int i, int imax, int x)
            {
                double tgA = Math.Tan(b * (x * (Math.PI / imax * i)));
                if ((tgA < Math.PI / 2) && (tgA > -1 * (Math.PI / 2)))
                    return Math.Round(a * tgA);
                else 
                    return 0;
            }
        }
        public class CtngA : IStrategy
        {
            public double Function(int a, double b, int i, int imax, int x)
            {
                double ctgA = 1 / Math.Tan(b * (x * (Math.PI / imax * i)));
                if ((ctgA < Math.PI / 2) && (ctgA > -1 * (Math.PI / 2)))
                    return Math.Round(a * ctgA);
                else
                    return 0;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) 
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
