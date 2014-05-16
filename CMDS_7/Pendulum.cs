using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace CMDS_7
{
    class Pendulum
    {
        private PointF start = new PointF((float)0.0, (float)0.0),
            end = new PointF((float)0.0, (float)0.0);

        private double length = 0.0;
        Pen pen = new Pen(Brushes.DarkBlue, 10);

        private double mass = 0.0;

        private double Fi = 0.0;

        public Pendulum()
        {
            this.length=35;
            mass = 10;
            this.start = new PointF((float)0.0, (float)0.0);
            this.end = new PointF((float)0.0, (float)length);
        }

        public Pendulum(PointF start, PointF end)
        {
            this.start = start;
            this.end = end;
        }

        public Pendulum(PointF start, double length, double fi)
        {
            this.start = start;
            this.length = length;
            this.Fi = fi;
        }

        public Pendulum(double startX, double startY, double endX, double endY)
        {
            this.start = new PointF((float)startX, (float)startY);
            this.end = new PointF((float)endX, (float)endY);
        }

        public Pendulum setCoord(double x, double y){
            this.FillPenulumVars();
            this.end.X = Math.Abs(x - this.start.X) <= length ? (float)x :
                x > this.start.X ? (float)(this.start.X + length) : (float)(this.start.X - length);
            float tmp = (float)(start.Y + (float)Math.Sqrt((length * length - Math.Pow(start.X - end.X, 2))));
            this.end.Y = tmp < this.start.Y ? this.start.Y : tmp;
            return this;
        }

        public Pendulum setStartPos(double x, double y)
        {
            this.start.X = (float)x;
            this.start.Y = (float)y;
            return this;
        }
        public Pendulum setMass(double mass)
        {
            this.mass = mass;
            return this;
        }

        public PointF getCenter()
        {
            this.FillPenulumVars();
            PointF result = new PointF((float)(this.start.X + (this.end.X - this.start.X) / 2.0), (float)(this.start.Y + (this.end.Y - this.start.Y) / 2.0));
            //if (result != result)         //типо корень отрицательного числа -_-
            //    MessageBox.Show("Fail -__-");
            return result;
        }

        public Pendulum drawPendulum(Graphics g)
        {
            float rad = 10.0F;
            g.DrawEllipse(new Pen(Brushes.DarkBlue), (float)(this.start.X - rad / 2.0F), (float)(this.start.Y - rad / 2.0F), rad, rad);
            g.DrawLine(pen, start, end);
            return this;
        }
        public void FillPenulumVars()
        {
            if (length == 0)
                length = Math.Sqrt(Math.Pow((double)(start.X - end.X), 2) +
                    Math.Pow((double)(start.Y - end.Y), 2));
            //else if()
        }

        public PointF getEndPoint()
        {
            return this.end;
        }
        public PointF getStartPoint()
        {
            return this.start;
        }

    }
}
