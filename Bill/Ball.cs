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
        public int ballX { get; set; }
        public int ballY { get; set; }
        
        public int Number { get; set; }
        public Color BaseColor { get; set; }
        public int radius { get; set; }
        public float VelocityX = 0;
        public float VelocityY = 0;

        public Color[] colors = {
            Color.Yellow,
            Color.Blue,
            Color.Red,
            Color.Purple,
            Color.Orange,
            Color.Green,
            Color.Maroon,
            Color.Black
        };
        public Ball(int ballX, int ballY, int number, int radius)
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
        }
        public void Draw(Graphics g) {

            String stringNumber = Number.ToString();

            Brush b = new SolidBrush(BaseColor);
            Brush bString = new SolidBrush(Color.Black);
            Brush bWhite = new SolidBrush(Color.White);
            Font f = new Font("Arial", (float)(radius * 0.6), FontStyle.Bold, GraphicsUnit.Pixel);
            Pen p = new Pen(Color.Red, 2);


            // Calculate position to center the text (so chatgpt e napraveno centriranje na textot vo topkite)

            
            SizeF textSize = g.MeasureString(stringNumber, f);

            
            float textX = ballX - textSize.Width / 2;
            float textY = ballY - textSize.Height / 2;
            if (Number <= 8)
            {
                g.FillEllipse(b, ballX - radius, ballY - radius, 2 * radius, 2 * radius);
            }
            if (Number > 8 && Number < 16) {
                g.FillEllipse(bWhite, ballX - radius, ballY - radius, 2 * radius, 2 * radius);
                g.FillEllipse(b, ballX - radius, ballY- (radius+8)/2, 2*radius,  radius+8);
            }
            if (Number < 16) {
            g.FillEllipse(bWhite, ballX - (int)(radius * 0.5), ballY - (int)(radius * 0.5), radius, radius);
            g.DrawString(stringNumber, f, bString, textX, textY);
            }
            else
            {
                g.FillEllipse(bWhite, ballX - radius, ballY - radius, 2 * radius, 2 * radius);
                g.DrawEllipse(p, (int)(ballX - radius * 0.15), (int)(ballY - radius * 0.15), (int)(radius * 0.3), (int)(radius * 0.3));
            }
            p.Dispose();
            f.Dispose();
            b.Dispose();
            bString.Dispose();
            bWhite.Dispose();
        }
    }
}
