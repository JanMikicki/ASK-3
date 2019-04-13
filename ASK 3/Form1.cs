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
        Color analog_back_color = Color.WhiteSmoke;
        Color analog_contours = Color.Black;
        Brush analog_number = Brushes.Black;

        Timer t = new Timer();
        int WIDTH = 200, HEIGHT = 200, secHAND = 90, minHAND = 80, hrHAND = 50;

        //center
        int cx, cy;

        Bitmap bmp;
        Graphics g;
       

        public Form1()
        {
            InitializeComponent();
            this.Text = "Calculator";
            this.labelDate.Text = DateTime.Today.ToString("D");
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

        private void nightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(20, 29, 38);
            this.labelDigital.ForeColor = Color.WhiteSmoke;
            this.labelDate.ForeColor = Color.WhiteSmoke;
            this.analog_back_color = Color.FromArgb(20, 29, 38);

            this.analog_contours = Color.White;
            this.analog_number = Brushes.White;

            this.labelDigital.Font = new Font("Consolas", this.labelDigital.Font.Size);

            foreach (var b in this.panel1.Controls.OfType<Button>())
            {
                b.BackColor = Color.FromArgb(0, 191, 165);
                b.FlatAppearance.BorderColor = Color.FromArgb(0, 121, 107);


            }
            foreach (var b in this.panel2.Controls.OfType<Button>())
            {
                b.BackColor = Color.FromArgb(36, 52, 71);
                b.ForeColor = Color.FromArgb(189, 199, 193);
            }
            this.calcDisplay.BackColor = Color.FromArgb(20, 29, 38);
            this.calcDisplay.ForeColor = Color.FromArgb(189, 199, 193);
            this.equation_label.ForeColor = Color.FromArgb(189, 199, 193);

        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            this.labelDigital.ForeColor = Color.Black;
            this.labelDate.ForeColor = Color.Black;
            this.analog_back_color = Color.WhiteSmoke;

            this.analog_contours = Color.Black;
            this.analog_number = Brushes.Black;

            this.labelDigital.Font = new Font("MS Reference Sans Serif", this.labelDigital.Font.Size);
            
            foreach (var b in this.panel1.Controls.OfType<Button>())
            {
                b.BackColor = Color.Gainsboro;
                b.FlatAppearance.BorderColor = Color.DimGray;
                b.ForeColor = Color.Black;

            }
            foreach (var b in this.panel2.Controls.OfType<Button>())
            {
                b.BackColor = Color.White;
                b.ForeColor = Color.Black;
            }
            this.calcDisplay.BackColor = Color.White;
            this.calcDisplay.ForeColor = Color.Black;
            this.equation_label.ForeColor = Color.Black;

        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 98, 255);
            this.labelDigital.ForeColor = Color.Azure;
            this.labelDate.ForeColor = Color.Azure;

            this.analog_back_color = Color.FromArgb(0, 98, 255);
            this.analog_contours = Color.Azure;
            this.analog_number = Brushes.Azure;

            this.labelDigital.Font = new Font("Arial", this.labelDigital.Font.Size);

            foreach (var b in this.panel1.Controls.OfType<Button>())
            {
                b.BackColor = Color.FromArgb(5, 48, 173);
                b.FlatAppearance.BorderColor = Color.MidnightBlue;
                b.ForeColor = Color.Azure;

            }
            foreach (var b in this.panel2.Controls.OfType<Button>())
            {
                b.BackColor = Color.Azure;
                b.ForeColor = Color.MidnightBlue;
            }

            this.calcDisplay.BackColor = Color.AliceBlue;
            this.calcDisplay.ForeColor = Color.Black;
            this.equation_label.ForeColor = Color.Azure;
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            this.calcDisplay.Text = "0";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.calcDisplay.Text = "0";
            value = 0;
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

        private void button_Click(object sender, EventArgs e)
        {
            if (this.calcDisplay.Text == "0" || operator_clicked == true)
                this.calcDisplay.Clear();

            operator_clicked = false;
            Button b = (Button)sender;
            this.calcDisplay.Text = this.calcDisplay.Text + b.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //backcolor
            this.BackColor = Color.WhiteSmoke;

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
            g.Clear(analog_back_color);

            //draw circle
            g.DrawEllipse(new Pen(analog_contours, 1f), 0, 0, WIDTH, HEIGHT);

            //draw figure
            g.DrawString("12", new Font("Arial", 12), analog_number, new PointF(90, 2));
            g.DrawString("3", new Font("Arial", 12), analog_number, new PointF(182, 90));
            g.DrawString("6", new Font("Arial", 12), analog_number, new PointF(92, 182));
            g.DrawString("9", new Font("Arial", 12), analog_number, new PointF(0, 90));

            //second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(analog_contours, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(analog_contours, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load bmp in picturebox1
            pictureBoxAnalog.Image = bmp;
        
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
       
    }
}
