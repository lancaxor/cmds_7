using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMDS_7
{
    public partial class Form1 : Form
    {
        private bool running = false;
        private int penIndex = 0;
        double D;
        Pendulum pend1, pend2;
        Spring spring;
        bool bMoving = false;

        Brush   brush;
        Timer   timer;

        public Form1()
        {
            InitializeComponent();
            this.tbD.Text = (1.0).ToString();               //Initial input data
            this.tbL.Text = (1.0).ToString();

            brush = Brushes.Blue;                           //drawing

            pend1=new Pendulum(this.pictureBox.Left + 50.0, this.pictureBox.Top + 10.0,
                this.pictureBox.Left + 50.0, this.pictureBox.Top + 200.0);
            pend2=new Pendulum(this.pictureBox.Left + 200.0, this.pictureBox.Top + 10.0,
                this.pictureBox.Left + 200.0, this.pictureBox.Top + 200.0);

            spring = new Spring(pend1.getCenter(), pend2.getCenter());

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerIntervalHandler;
        }


        private void TimerIntervalHandler(Object sender, EventArgs e)
        {
            this.pictureBox.Refresh();
        }

        private bool ReadInpudData(out string errormsg)
        {
            errormsg = "";
            bool result = true;
            double L;
            if (!Double.TryParse(this.tbL.Text, out L))
            {
                errormsg += "Bad \"L\" value. Enter double number\n";
                result = false;
            }
            if (!Double.TryParse(this.tbD.Text, out D))
            {
                errormsg += "Bad \"d\" value. Enter double number\n";
                result = false;
            }
            return result;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            String errorMsg;
            if (!ReadInpudData(out errorMsg))
            {               //check input
                MessageBox.Show("Error input: " + errorMsg);
                return;
            }

            if (running)
            {               //do stop
                this.btnStartStop.Text = "Start";
                running = false;
                timer.Stop();
            }
            else
            {               //do start
                timer.Start();
                this.btnStartStop.Text = "Stop";
                running = true;
            }
        }

        private double[] RungeKuth(double deltaTime, double maxTime, double x0)             //RungeKuth Method
        {
            Stack<double> result = new Stack<double>();
            result.Push(x0);
            for (double i = 0; i < maxTime; i += deltaTime)
            {
                double k1 = deltaTime * ActionFunction(i, result.Peek()),
                    k2 = deltaTime * ActionFunction(i + deltaTime / 2, result.Peek() + 0.5 * k1),
                    k3 = deltaTime * ActionFunction(i + deltaTime / 2, result.Peek() + 0.5 * k2),
                    k4 = deltaTime * ActionFunction(i + deltaTime, result.Peek() + k3);
                result.Push(result.Peek() + (k1 + 2 * k2 + 2 * k3 + k4) / 6.0);
            }

            return result.ToArray<double>();
        }

        #region counting....
        private double ActionFunction(double time, double x)
        {
            double result = 0.0;
            return result;
        }

        private double getKinetic()
        {
            double result = 0.0;
            return result;
        }

        private double getPotential()
        {
            double result = 0.0;
            return result;
        }
        #endregion

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            pend1.drawPendulum(e.Graphics);
            pend2.drawPendulum(e.Graphics);
            spring.drawSpring(e.Graphics);            
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            bMoving = true;
            int bound = 7;
            if ((Math.Abs(e.X - (int)pend1.getEndPoint().X) < bound) && (Math.Abs(e.Y - (int)pend1.getEndPoint().Y) < bound))
                penIndex = 1;
            else if ((Math.Abs(e.X - (int)pend2.getEndPoint().X) < bound) && (Math.Abs(e.Y - (int)pend2.getEndPoint().Y) < bound))
                penIndex = 2;
            else penIndex = 0;
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMoving && e.X > this.pictureBox.Left && e.X < this.pictureBox.Right && e.Y > this.pictureBox.Top && e.Y < this.pictureBox.Bottom)
            {
                if (penIndex == 1)
                {
                    penIndex = 1;
                    pend1.setCoord(e.X, e.Y);
                    spring.setPosition(pend1.getCenter(), pend2.getCenter());
                    Refresh();
                }
                else if (penIndex == 2)
                {
                    penIndex = 2;
                    pend2.setCoord(e.X, e.Y);
                    spring.setPosition(pend1.getCenter(), pend2.getCenter());
                    Refresh();
                }

            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            penIndex = 0;
            bMoving = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.pictureBox.Width = this.Width - 10;
        }
    }

}
