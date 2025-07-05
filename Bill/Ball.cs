using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Bill
{
    public class Ball
    {
        public double ballX { get; set; }
        public double ballY { get; set; }
        
        public int Number { get; set; }
        public Color BaseColor { get; set; }
        public int radius { get; set; }
        public double VelocityX = 0;
        public double VelocityY = 0;
        public bool fallen = false;

        public Point[] points { get; set; }


        public Color[] colors = {
            Color.FromArgb(249, 214, 23),
            Color.FromArgb(32, 32, 185),
            Color.FromArgb(198, 27, 27),
            Color.FromArgb(85, 0, 128),
            Color.FromArgb(242, 128, 21),
            Color.FromArgb(66, 163, 66),
            Color.Maroon,
            Color.Black
        };
        public Ball(double ballX, double ballY, int number, int radius, Point[] points)
        {
            this.ballX = ballX;
            this.ballY = ballY;
            Number = number;
            this.radius = radius;
            if (Number <= 15) { 
                BaseColor = colors[(Number-1)%8];
            }
            else
            {
                BaseColor = Color.White;
            }
            this.points = points;
        }

        public void Draw(Graphics g)
        {

            String stringNumber = Number.ToString();

            Brush b = new SolidBrush(BaseColor);
            Brush bString = new SolidBrush(Color.Black);
            Brush bWhite = new SolidBrush(Color.White);
            Font f = new Font("Arial", (float)(radius * 0.6), FontStyle.Bold, GraphicsUnit.Pixel);
            Pen p = new Pen(Color.FromArgb(198, 27, 27), 1);


            // Calculate position to center the text (so chatgpt e napraveno centriranje na textot vo topkite)

            
            SizeF textSize = g.MeasureString(stringNumber, f);

            
            double textX = ballX - textSize.Width / 2;
            double textY = ballY - textSize.Height / 2;
            if (Number <= 8)
            {
                g.FillEllipse(b, (float)ballX - radius, (float)ballY - radius, 2 * radius, 2 * radius);
            }
            if (Number > 8 && Number < 16) {
                g.FillEllipse(bWhite, (float)ballX - radius, (float)ballY - radius, 2 * radius, 2 * radius);
                g.FillEllipse(b, (float)ballX - (float)radius, (float)ballY - (float)((radius+8)/2), 2*radius,  radius+8);
            }
            if (Number < 16) {
                g.FillEllipse(bWhite, (float)ballX - (float)(radius * 0.5), (float)ballY - (float)(radius * 0.5), radius, radius);
                g.DrawString(stringNumber, f, bString, (float)textX, (float)textY);
            }
            else
            {
                g.FillEllipse(bWhite, (float)ballX - radius, (float)ballY - radius, 2 * radius, 2 * radius);
                g.DrawEllipse(p, (float)ballX - radius * 0.15f+1, ((float)ballY - radius * 0.15f)+1, radius * 0.3f, radius * 0.3f);
            }
            p.Dispose();
            f.Dispose();
            b.Dispose();
            bString.Dispose();
            bWhite.Dispose();
        }
        public void Move()
        {
            ballX += VelocityX;
            ballY += VelocityY;

            VelocityX *= 0.98;
            VelocityY *= 0.98;

            
            if (Math.Abs(VelocityY) + Math.Abs(VelocityX) <= 0.1)
            {
                VelocityX = 0;
                VelocityY = 0;
            }

            foreach (Point point in points)
            {
                if (Math.Abs(ballX - point.X) < 30 && Math.Abs(ballY - point.Y) < 30) // dovolno e centarot na masa da e vo dupka za da padne
                {
                    VelocityX = 0;
                    VelocityY = 0;
                    fallen = true;
                }
            }

            if (ballX - radius < points[0].X)
            {
                ballX = points[0].X + radius;
                VelocityX *= -1;
            }
            if (ballX + radius > points[3].X)
            {
                ballX = points[3].X - radius;
                VelocityX *= -1;
            }
            if (ballY - radius < points[0].Y)
            {
                ballY = points[0].Y + radius;
                VelocityY *= -1;
            }
            if (ballY + radius > points[3].Y)
            {
                ballY = points[3].Y - radius;
                VelocityY *= -1;
            }
        }
    }
}
