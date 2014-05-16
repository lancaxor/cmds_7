using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMDS_7
{
    class Spring
    {
        private PointF start, end;
        Pen pen = new Pen(Brushes.Blue, 5);


        public Spring()
        {
            start = new PointF((float)0.0, (float)0.0);
            end = new PointF((float)10.0, (float)10.0);
        }

        public Spring(PointF startPos, PointF endPos)
        {
            this.start = startPos;
            this.end = endPos;
        }

        public Spring setPosition(PointF start, PointF end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        public Spring setPosition(double startX, double startY, double endX, double endY)
        {
            this.start.X = (float)startX;
            this.start.Y = (float)startY;
            this.end.X = (float)endX;
            this.end.Y = (float)endY;
            return this;
        }

        public Spring drawSpring(Graphics g)
        {
            g.DrawLine(pen, start, end);
            return this;
        }
    }
}
