using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill
{
    public class Scene
    {
        public List<Ball> balls = new List<Ball>();
        public List<Hole> holes = new List<Hole>();
        public int ScreenWidth;
        public int ScreenHeight;
        public int TableWidth;
        public int TableHeight;
        public int BlankSpaceWidth;
        public int BlankSpaceHeight;
        public int konstantaHeight;
        public int konstantaWidth;
        public Point[] points;
        public Scene(int ScreenWidth, int ScreenHeight)
        {
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            this.TableWidth = (int)(ScreenWidth * 0.60);
            this.TableHeight = (int)(ScreenHeight * 0.60);

            this.BlankSpaceWidth = (int)(ScreenWidth * 0.2);
            this.BlankSpaceHeight = (int)(ScreenHeight * 0.2);

            this.konstantaHeight = 30; //const za poramnuvanje
            this.konstantaWidth = 10;


            Point TopLeft = new Point((int)(ScreenWidth - TableWidth - BlankSpaceWidth - konstantaWidth), (int)(ScreenHeight - TableHeight - BlankSpaceHeight - konstantaHeight));
            Point BottomLeft = new Point((int)(ScreenWidth - TableWidth - BlankSpaceWidth - konstantaWidth), (int)(TableHeight + BlankSpaceHeight - konstantaHeight));
            Point TopRight = new Point((int)(TableWidth + BlankSpaceWidth - konstantaWidth), (int)(ScreenHeight - TableHeight - BlankSpaceHeight - konstantaHeight));
            Point BottomRight = new Point((int)(TableWidth + BlankSpaceWidth - konstantaWidth), (int)(TableHeight + BlankSpaceHeight - konstantaHeight));
            Point MiddleTop = new Point((int)((TableWidth + BlankSpaceWidth - konstantaWidth) + (ScreenWidth - TableWidth - BlankSpaceWidth - konstantaWidth)) / 2, (int)(ScreenHeight - TableHeight - BlankSpaceHeight - konstantaHeight));
            Point MiddleBottom = new Point((int)((TableWidth + BlankSpaceWidth - konstantaWidth) + (ScreenWidth - TableWidth - BlankSpaceWidth - konstantaWidth)) / 2, (int)(TableHeight + BlankSpaceHeight - konstantaHeight));

            Point[] tocki =
            {
                TopLeft,
                MiddleTop,
                TopRight,
                BottomRight,
                MiddleBottom,
                BottomLeft
            };
            this.points = tocki;

            populate();
        }

        public void populate()
        {

            Point TopLeft = points[0];

            int BallRadius = (int)(0.03 * (TableWidth + TableHeight) / 2);
            int BallX = (int)(TopLeft.X+TableWidth*0.6);
            int BallY = (int)(TopLeft.Y + TableHeight/2);

            int InitialBallY = BallY;

            int konstantaNaPomestuvanje = (int)(BallRadius * Math.Sqrt(3))+3;
            int counter = 1;
            for(int i=1; i<= 5; i++)
            {
                
                for (int j = 0; j < i; j++)
                {
                    balls.Add(new Ball(BallX, BallY, counter, BallRadius));
                    counter++;
                    BallY = BallY - 2 * BallRadius;
                }
                BallY = InitialBallY + BallRadius * i;
                BallX += konstantaNaPomestuvanje;
            }
            balls.Add(new Ball((int)(TopLeft.X + TableWidth / 6), InitialBallY, 16, BallRadius));
        }
        
        public void resizeBalls()
        {
            
            foreach (Ball ball in balls)
            {
                int BallRadius = (int)((TableWidth + TableHeight) *0.015);
                ball.radius = BallRadius;
            }
        }
        public void Draw(Graphics g)
        {
            DrawTable(g);
            DrawBalls(g);
        }

        public void DrawBalls(Graphics g)
        {
            foreach (Ball ball in balls)
            {
                ball.Draw(g);
            }
        }
        public void DrawTable(Graphics g)
        {
            

            Brush bGreen = new SolidBrush(Color.DarkGreen);
            Brush bBlack = new SolidBrush(Color.Black);
            Pen pWhite = new Pen(Color.White, 2);
            Brush bBrown = new SolidBrush(Color.Brown);

            int koef = 35;
            Point[] Edge =
            {
                new Point(points[0].X-koef, points[0].Y-koef),
                new Point(points[2].X+koef, points[2].Y-koef),
                new Point(points[3].X+koef, points[3].Y+koef),
                new Point(points[5].X-koef, points[5].Y+koef)
            };
            g.FillPolygon(bBrown, Edge);
            g.FillPolygon(bGreen, points);

            holes = new List<Hole>();

            int HoleRadius = (int)(0.05 * (TableWidth+TableHeight)/2);

            foreach (Point point in points)
            {
                Hole hole = new Hole(point.X, point.Y, HoleRadius);
                hole.Draw(g);
                holes.Add(hole);
            }

            Point QuarerPointTop = new Point((int)(points[0].X + TableWidth / 4)+40, points[0].Y);
            Point QuarerPointBottom = new Point((int)(points[0].X + TableWidth / 4)+40, points[3].Y);

            g.DrawLine(pWhite, QuarerPointTop, QuarerPointBottom);

            bGreen.Dispose();
            bBlack.Dispose();
            pWhite.Dispose();
            bBrown.Dispose();
           
        }

    }
}
