using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bill
{
    public class Scene
    {
        private Point lastMousePos;
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
        public Point[] quarterPoints;

        public bool mouseDown = false;
        public bool moving = false;
        public int power = 0;
        public bool cueBallPlaced = false;
        public bool firstHit = true;
        public Scene(int ScreenWidth, int ScreenHeight)
        {
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            this.TableWidth = (int)(ScreenWidth * 0.70);
            this.TableHeight = (int)(ScreenHeight * 0.60);

            this.BlankSpaceWidth = (int)(ScreenWidth * 0.15);
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

            Point QuarterPointTop = new Point((int)(points[0].X + TableWidth / 4), points[0].Y);
            Point QuarterPointBottom = new Point((int)(points[0].X + TableWidth / 4), points[3].Y);

            Point[] tocki2 =
            {
                QuarterPointTop,
                QuarterPointBottom
            };
            this.quarterPoints = tocki2;

            populate();
        }
        public void powerUp()
        {
            if (moving) {
                return;
            }
            if(mouseDown && power<100 && !cueBallPlaced)
            {
                power += 10;
            }
        }
        public void populate()
        {

            Point TopLeft = points[0];

            int BallRadius = 16;
            double BallX = TopLeft.X+TableWidth*0.7;
            double BallY = TopLeft.Y + TableHeight/2;

            double InitialBallY = BallY;

            int konstantaNaPomestuvanje = (int)(BallRadius * Math.Sqrt(3))+3;
            int counter = 1;
            for(int i=1; i<= 5; i++)
            {
                
                for (int j = 0; j < i; j++)
                {
                    balls.Add(new Ball(BallX, BallY, counter, BallRadius, points));
                    counter++;
                    BallY = BallY - 2 * BallRadius;
                }
                BallY = InitialBallY + BallRadius * i;
                BallX += konstantaNaPomestuvanje;
            }
            balls.Add(new Ball(TopLeft.X + TableWidth / 6, InitialBallY, 16, BallRadius, points));
            balls[15].fallen = true;
        }

        public bool placeCueBall(Point mousePos)
        {
            Ball cueBall = balls[15];
            if (cueBall.fallen && !cueBallPlaced)
            {
                if (!IsPlacable(mousePos, cueBall.radius))
                {
                    return false;
                }
                if (firstHit)
                {
                    if (mousePos.X + cueBall.radius > points[0].X &&
                        mousePos.X + cueBall.radius < quarterPoints[1].X
                        && mousePos.Y + cueBall.radius > points[0].Y
                        && mousePos.Y + cueBall.radius < quarterPoints[1].Y)
                    {
                        cueBall.ballX = mousePos.X;
                        cueBall.ballY = mousePos.Y;
                        cueBall.fallen = false;
                        firstHit = false;
                        return true;
                    }
                    else return false;
                }
                else
                {
                    cueBall.ballX = mousePos.X;
                    cueBall.ballY = mousePos.Y;
                    cueBall.fallen = false;
                    return true;
                }  
            }
            return false;
        }

        public bool IsPlacable(Point mousePos, int radius)
        {
            foreach (Hole hole in holes)
            {
                if (((mousePos.X - hole.holeX) * (mousePos.X - hole.holeX) + (mousePos.Y - hole.holeY) * (mousePos.Y - hole.holeY) <= 4 * radius * hole.radius))
                    return false;
            }

            foreach (Ball ball in balls)
            {
                if ((mousePos.X - ball.ballX) * (mousePos.X - ball.ballX) + (mousePos.Y - ball.ballY) * (mousePos.Y - ball.ballY) <= 4 * radius * ball.radius)
                    return false;
            }
            if (mousePos.X - radius <= points[0].X ||
                mousePos.X + radius >= points[3].X ||
                mousePos.Y - radius <= points[0].Y ||
                mousePos.Y + radius >= points[3].Y)
            {
                return false;
            }
            return true;
        }

        public bool CollidesWith(Ball b1, Ball b2)
        {
            return !((b1.ballX - b2.ballX) * (b1.ballX - b2.ballX) + (b1.ballY - b2.ballY) * (b1.ballY - b2.ballY) <= 4 * b1.radius * b2.radius);
        }

        public void resizeBalls()
        {
            
            foreach (Ball ball in balls)
            {
                int BallRadius = (int)((TableWidth + TableHeight) *0.015);
                ball.radius = BallRadius;
            }
        }
        public void Draw(Graphics g, Point mousePos)
        {
            DrawTable(g);
            DrawBalls(g);
            DrawCue(g, mousePos);
        }

        public void DrawBalls(Graphics g)
        {
            foreach (Ball ball in balls)
            {
                if (!ball.fallen) ball.Draw(g);
            }
        }
        public void DrawTable(Graphics g)
        {
            

            Brush bGreen = new SolidBrush(Color.DarkGreen);
            Brush bBlack = new SolidBrush(Color.Black);
            Pen pWhite = new Pen(Color.Green, 2);
            Brush bBrown = new SolidBrush(Color.FromArgb(91, 52, 21));

            int koef = 50;
            Point[] Edge =
            {
                new Point(points[0].X-koef, points[0].Y),
                new Point(points[0].X, points[0].Y-koef),
                new Point(points[2].X, points[2].Y-koef),
                new Point(points[2].X+koef, points[2].Y),
                new Point(points[3].X+koef, points[3].Y),
                new Point(points[3].X, points[3].Y+koef),
                new Point(points[5].X, points[5].Y+koef),
                new Point(points[5].X-koef, points[5].Y)
            };
            g.FillPolygon(bBrown, Edge);

            foreach(Point point in points)
            {
                g.FillEllipse(bBrown, point.X-koef, point.Y-koef, koef*2, koef*2);
            }

            g.FillPolygon(bGreen, points);

            holes = new List<Hole>();

            //int HoleRadius = (int)(0.045 * (TableWidth+TableHeight)/2);
            int HoleRadius = 30;

            foreach (Point point in points)
            {
                Hole hole = new Hole(point.X, point.Y, HoleRadius);
                hole.Draw(g);
                holes.Add(hole);
            }

            g.DrawLine(pWhite, quarterPoints[0], quarterPoints[1]);

            bGreen.Dispose();
            bBlack.Dispose();
            pWhite.Dispose();
            bBrown.Dispose();
           
        }
        public void DrawCue(Graphics g, Point mousePos)
        {
            if (moving)
            {
                return;
            }

            Ball cueBall = balls[15];

            if (cueBall.fallen)
            {
                return;
            }

            Point cueBallCenter = new Point((int)cueBall.ballX, (int)cueBall.ballY);

            Point currentPos;

            if (mouseDown && !cueBallPlaced)
            { 
                currentPos = lastMousePos;
            }
            else
            {
                currentPos = mousePos;
                lastMousePos = mousePos;
            }

            int dx = currentPos.X - cueBallCenter.X;
            int dy = currentPos.Y - cueBallCenter.Y;
            if (dx == 0 && dy == 0)
            {
                dx = 1; 
            }
            double length = Math.Sqrt(dx * dx + dy * dy);

            double nx = dx / length;
            double ny = dy / length;

            int offsetFromBall = 20 + power;
            int cueLength = 425;

            PointF tipPoint = new PointF(
                cueBallCenter.X - (float)(nx * offsetFromBall),
                cueBallCenter.Y - (float)(ny * offsetFromBall)
            );

            PointF basePoint = new PointF(
                tipPoint.X - (float)(nx * cueLength),
                tipPoint.Y - (float)(ny * cueLength)
            );

            PointF midPoint = new PointF(
                tipPoint.X - (float)(nx * (cueLength * 0.7)),
                tipPoint.Y - (float)(ny * (cueLength * 0.7))
            );

            PointF guideLinePoint = new PointF(
                cueBallCenter.X + (float)(nx * 20),
                cueBallCenter.Y + (float)(ny * 20)
            );

            Pen tipPen = new Pen(Color.BurlyWood, 7);
            g.DrawLine(tipPen, tipPoint, midPoint);

            Pen basePen = new Pen(Color.SaddleBrown, 8);
            g.DrawLine(basePen, midPoint, basePoint);

            PointF ghostBall = new PointF((float)guideLinePoint.X + (float)(nx * 20), (float)guideLinePoint.Y + (float)(ny * 20));

            if (!((currentPos.X - cueBall.ballX) * (currentPos.X - cueBall.ballX) + (currentPos.Y - cueBall.ballY) * (currentPos.Y - cueBall.ballY) <= 4 * cueBall.radius * cueBall.radius))
            {
                while (IsPlacable(new Point((int)ghostBall.X, (int)ghostBall.Y), cueBall.radius))
                {
                    ghostBall.X += (float)(nx);
                    ghostBall.Y += (float)(ny);
                }

                Pen linePen = new Pen(Color.White, 3);
                linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawLine(linePen, guideLinePoint, ghostBall);
                g.DrawEllipse(linePen, ghostBall.X - cueBall.radius, ghostBall.Y - cueBall.radius, cueBall.radius * 2, cueBall.radius * 2);

                linePen.Dispose();
            }

            tipPen.Dispose();
            basePen.Dispose();
        }
        public void Strike()
        {
            if (moving) { // staveno samo poradi power=0, prethodno ako kliknes za vreme na dvizenje togas power se setira na 0 i pravi problem
                return;
            }

            Ball cueBall = balls[15];

            Point cueBallCenter = new Point((int)cueBall.ballX, (int)cueBall.ballY);

            int dx = lastMousePos.X - cueBallCenter.X;
            int dy = lastMousePos.Y - cueBallCenter.Y;
            if (dx == 0 && dy == 0)
            {
                dx = 1;
            }
            double length = Math.Sqrt(dx * dx + dy * dy);

            double nx = dx / length;
            double ny = dy / length;

            cueBall.VelocityX = nx * power * 0.3; // koeficient poradi sto 100 px na sekunda da se dvizi topka, ednostavno ne izgleda dobro :D
            cueBall.VelocityY = ny * power * 0.3;
            power = 0;
        }
        public void CheckIfMoving()
        {
            foreach (Ball ball in balls)
            {
                if (ball.VelocityX != 0 || ball.VelocityY != 0) {
                    moving = true;
                    return;
                }
                else
                {
                    moving = false;
                }
            }
        }
        public void HandleBallCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                Ball b1 = balls[i];
                if (b1.fallen) 
                { 
                    continue;
                }

                for (int j = i + 1; j < balls.Count; j++)
                {
                    Ball b2 = balls[j];
                    if (b2.fallen) 
                    {
                        continue;
                    }
                    double dx = b2.ballX - b1.ballX;
                    double dy = b2.ballY - b1.ballY;
                    double distance = Math.Sqrt(dx * dx + dy * dy);
                    double collisionDistance = b1.radius + b2.radius;

                    if (distance < collisionDistance && distance > 0.01)
                    {
                        //оверлап е направено со помош на ChatGPT, направено за да не може 2 топки да бидат една врз друга
                        double overlap = 0.5 * (collisionDistance - distance);
                        double nx = dx / distance;
                        double ny = dy / distance;

                        b1.ballX -= nx * overlap;
                        b1.ballY -= ny * overlap;
                        b2.ballX += nx * overlap;
                        b2.ballY += ny * overlap;

                        double vx = b2.VelocityX - b1.VelocityX;
                        double vy = b2.VelocityY - b1.VelocityY;
                        double Corelation = vx * nx + vy * ny;


                        if (Corelation < 0)
                        { 
                            b1.VelocityX += nx * Corelation;
                            b1.VelocityY += ny * Corelation;
                            b2.VelocityX -= nx * Corelation;
                            b2.VelocityY -= ny * Corelation;
                        }
                    }
                }
            }
        }
    }
}
