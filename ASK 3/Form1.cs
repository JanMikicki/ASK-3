using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASK_3
{
    public partial class Form1 : Form
    {
        Double value = 0;
        String operation = "";
        bool operator_clicked = false;

        Timer t = new Timer();
        int WIDTH = 200, HEIGHT = 200, secHAND = 90, minHAND = 80, hrHAND = 50;

        //center
        int cx, cy;

        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void cyfrowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pictureBoxAnalog.Hide();
            this.labelDigital.Show();
        }

        private void analogowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pictureBoxAnalog.Show();
            this.labelDigital.Hide();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            this.labelDigital.ForeColor = Color.WhiteSmoke;
           
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.AntiqueWhite;
            this.labelDigital.ForeColor = Color.Black;
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            this.calcDisplay.Text = "0";
        }

        private void operator_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            operation = b.Text;
            value = Double.Parse(this.calcDisplay.Text);
            operator_clicked = true;
            equation_label.Text = value + " " + operation;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            equation_label.Text = "";
            switch (operation)
            {
                case "+":
                    this.calcDisplay.Text = (value + Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "-":
                    this.calcDisplay.Text = (value - Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "*":
                    this.calcDisplay.Text = (value * Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "/":
                    this.calcDisplay.Text = (value / Double.Parse(calcDisplay.Text)).ToString();
                    break;
                default:
                    break;
            }            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.calcDisplay.Text = "0";
            value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //backcolor
            this.BackColor = Color.White;

            //timer
            t.Interval = 1000;      //in millisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            //create graphics
            g = Graphics.FromImage(bmp);

            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            //clear
            g.Clear(Color.White);

            //draw circle
            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, WIDTH, HEIGHT);

            //draw figure
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(90, 2));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(182, 90));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(92, 182));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 90));

            //second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load bmp in picturebox1
            pictureBoxAnalog.Image = bmp;

            //disp time
            this.Text = "Analog Clock -  " + hh + ":" + mm + ":" + ss;

            this.labelDigital.Text = DateTime.Now.ToString("T");
            //dispose
            g.Dispose();
        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;   //each minute and second make 6 degree

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            //each hour makes 30 degree
            //each min makes 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (this.calcDisplay.Text == "0" || operator_clicked == true)
                this.calcDisplay.Clear();

            operator_clicked = false;
            Button b = (Button)sender;
            this.calcDisplay.Text = this.calcDisplay.Text + b.Text;
        }
    }
}
