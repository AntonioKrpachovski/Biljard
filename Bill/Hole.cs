using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill
{
    public class Hole
    {
        public int holeX { get; set; }
        public int holeY { get; set; }
        public int radius { get; set; }
        Color color = Color.Black;

        public Hole(int holeX, int holeY, int radius)
        {
            this.holeX = holeX;
            this.holeY = holeY;
            this.radius = radius;
        }

        public void Draw(Graphics g)
        {

            
            Brush b = new SolidBrush(color);
            
            g.FillEllipse(b, holeX - radius, holeY - radius, 2 * radius, 2 * radius);

            b.Dispose();

        }
    }
}
