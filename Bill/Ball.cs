using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill
{
    public class Ball
    {
        public int ballX { get; set; }
        public int ballY { get; set; }
        
        public string Number { get; set; }
        public Color BaseColor { get; set; }
        public int radius = 12;
        public float VelocityX = 0;
        public float VelocityY = 0;

        public Ball(int ballX, int ballY, string number, Color baseColor)
        {
            this.ballX = ballX;
            this.ballY = ballY;
            Number = number;
            BaseColor = baseColor;
        }
        public void Draw(Graphics g) {
            Brush b = new SolidBrush(BaseColor);
            Brush bString = new SolidBrush(Color.Black);
            Font f = new Font("Arial", (float)(radius * 0.9), FontStyle.Bold, GraphicsUnit.Pixel);
            g.FillEllipse(b, ballX - radius, ballY - radius, 2 * radius, 2 * radius);


            // Calculate position to center the text (so chatgpt e napraveno centriranje na textot vo topkite)

            SizeF textSize = g.MeasureString(Number, f);

            
            float textX = ballX - textSize.Width / 2;
            float textY = ballY - textSize.Height / 2;

            g.DrawString(Number, f, bString, textX, textY);

        }
    }
}
