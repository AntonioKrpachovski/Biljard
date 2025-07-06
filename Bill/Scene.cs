using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public int radius = 16;
        public Point[] points;
        public Point[] quarterPoints;

        public bool mouseDown = false;
        public bool moving = false;
        public int power = 0;
        public bool cueBallPlaced = false;
        public bool firstHit = true;

        public bool whiteBallJustFallen = false;
        public int failsCounter = 0;
        public bool gameOverFlag = false;
        public bool ball8Fallen = false;
        public bool DarkMode = false;

        public Scene(int ScreenWidth, int ScreenHeight)
        {
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            this.TableWidth = (int)(ScreenWidth * 0.70);
            this.TableHeight = (int)(ScreenHeight * 0.60);

            this.BlankSpaceWidth = (int)(ScreenWidth * 0.15);
            this.BlankSpaceHeight = (int)(ScreenHeight * 0.2);

            this.konstantaHeight = 25; //const za poramnuvanje
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

            double BallX = TopLeft.X+TableWidth*0.7;
            double BallY = TopLeft.Y + TableHeight/2;

            double InitialBallY = BallY;

            int konstantaNaPomestuvanje = (int)(radius * Math.Sqrt(3))+3;
            int counter = 1;
            for(int i=1; i<= 5; i++)
            {
                
                for (int j = 0; j < i; j++)
                {
                    balls.Add(new Ball(BallX, BallY, counter, radius, points, DarkMode));
                    counter++;
                    BallY = BallY - 2 * radius;
                }
                BallY = InitialBallY + radius * i;
                BallX += konstantaNaPomestuvanje;
            }
            balls.Add(new Ball(0, 0, 16, radius, points, DarkMode));
            balls[15].fallen = true;
        }

        public bool placeCueBall(Point mousePos)
        {
            Ball cueBall = balls[15];
            if (cueBall.fallen && !cueBallPlaced)
            {
                if (!IsPlacable(mousePos) || moving)
                {
                    return false;
                }
                if (firstHit)
                {
                    if (mousePos.X + radius > points[0].X &&
                        mousePos.X + radius < quarterPoints[1].X
                        && mousePos.Y + radius > points[0].Y
                        && mousePos.Y + radius < quarterPoints[1].Y)
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

        public bool IsOnTable(Point mousePos)
        {
            return mousePos.X > points[0].X - holes[0].radius * 2 &&
                    mousePos.X < points[3].X + holes[0].radius * 2 &&
                    mousePos.Y > points[0].Y - holes[0].radius * 2 &&
                    mousePos.Y < points[3].Y + holes[0].radius * 2;
        }

        public bool IsPlacable(Point mousePos)
        {
            foreach (Hole hole in holes)
            {
                if ((mousePos.X - hole.holeX) * (mousePos.X - hole.holeX) + (mousePos.Y - hole.holeY) * (mousePos.Y - hole.holeY) <= 4 * radius * hole.radius)
                    return false;
            }

            if (firstHit)
            {
                if (!(mousePos.X + radius > points[0].X &&
                    mousePos.X + radius < quarterPoints[1].X
                    && mousePos.Y + radius > points[0].Y
                    && mousePos.Y + radius < quarterPoints[1].Y))
                {
                    return false;
                }
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
            return !((b1.ballX - b2.ballX) * (b1.ballX - b2.ballX) + (b1.ballY - b2.ballY) * (b1.ballY - b2.ballY) <= 4 * radius * radius);
        }

        //public void resizeBalls()
        //{
        //    foreach (Ball ball in balls)
        //    {
        //        int BallRadius = (int)((TableWidth + TableHeight) *0.015);
        //        ball.radius = BallRadius;
        //    }
        //}

        public void Draw(Graphics g, Point mousePos)
        {
            
            DrawTable(g);
            DisplayMiniTable(g);
            DisplayFallen(g);
            DrawBalls(g);
            DrawGhostBall(g, mousePos);
            DrawCue(g, mousePos);
            DrawLives(g);
        }

        public void DrawBalls(Graphics g)
        {
            for (int i=0; i<15; i++)
            {
                balls[i].Draw(g, DarkMode);
            }
            if (!balls[15].fallen)
            {
                balls[15].Draw(g, DarkMode);
            }
        }

        public void DrawTable(Graphics g)
        {
            Brush bBlack = new SolidBrush(Color.Black);
            Pen pWhite;
            Brush bBrown;

            if (DarkMode)
            {
                bBrown = new SolidBrush(Color.FromArgb(80, 77, 140));
            }
            else
            {
                bBrown = new SolidBrush(Color.FromArgb(91, 52, 21));
            }

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

            foreach (Point point in points)
            {
                g.FillEllipse(bBrown, point.X - koef, point.Y - koef, koef * 2, koef * 2);
            }

            if (DarkMode)
            {
                Brush bBlue = new SolidBrush(Color.FromArgb(48, 46, 87));
                g.FillPolygon(bBlue, points);
                bBlue.Dispose();
                pWhite = new Pen(Color.FromArgb(61, 61, 105), 2);
            }
            else
            {
                Brush bGreen = new SolidBrush(Color.DarkGreen);
                g.FillPolygon(bGreen, points);
                bGreen.Dispose();
                pWhite = new Pen(Color.Green, 2);
            }

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

            bBlack.Dispose();
            pWhite.Dispose();
            bBrown.Dispose();
        }

        public void DrawGhostBall(Graphics g, Point mousePos)
        {
            if (gameOverFlag || moving || failsCounter == 4)
            {
                return;
            }
            if (balls[15].fallen && !moving && !mouseDown)
            {
                if (IsOnTable(mousePos))
                {
                    Pen linePen;
                    if (IsPlacable(mousePos))
                    {
                        linePen = new Pen(Color.White, 3);
                    }
                    else
                    {
                        linePen = new Pen(Color.Red, 3);
                    }
                    linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(linePen, mousePos.X - radius, mousePos.Y - radius, radius * 2, radius * 2);

                    linePen.Dispose();
                }
            }
        }

        public void DrawCue(Graphics g, Point mousePos)
        {
            Ball cueBall = balls[15];

            if (moving || cueBall.fallen || gameOverFlag)
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

            Pen tipPen;
            Pen basePen;

            if (DarkMode)
            {
                tipPen = new Pen(Color.FromArgb(135, 199, 222), 7);
                basePen = new Pen(Color.FromArgb(82, 125, 164), 8);
            }
            else
            {
                tipPen = new Pen(Color.BurlyWood, 7);
                basePen = new Pen(Color.SaddleBrown, 8);
            }

            g.DrawLine(tipPen, tipPoint, midPoint);
            g.DrawLine(basePen, midPoint, basePoint);

            PointF ghostBall = new PointF((float)guideLinePoint.X + (float)(nx * 20), (float)guideLinePoint.Y + (float)(ny * 20));

            if (!((currentPos.X - cueBall.ballX) * (currentPos.X - cueBall.ballX) + (currentPos.Y - cueBall.ballY) * (currentPos.Y - cueBall.ballY) <= 1.6f * 1.6f * radius * radius))
            {
                while (IsPlacable(new Point((int)ghostBall.X, (int)ghostBall.Y)))
                {
                    ghostBall.X += (float)nx;
                    ghostBall.Y += (float)ny;
                }

                Pen linePen = new Pen(Color.White, 3);
                linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawLine(linePen, guideLinePoint, ghostBall);
                g.DrawEllipse(linePen, ghostBall.X - 0.8f * radius, ghostBall.Y - 0.8f * radius, radius * 1.6f, radius * 1.6f);

                linePen.Dispose();
            }

            tipPen.Dispose();
            basePen.Dispose();
        }
        public void Strike(Point mousePos)
        {
            if (moving) { //staveno samo poradi power=0, prethodno ako kliknes za vreme na dvizenje togas power se setira na 0 i pravi problem
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

            cueBall.VelocityX = nx * power * 0.35; // koeficient poradi sto 100 px na sekunda da se dvizi topka, ednostavno ne izgleda dobro :D
            cueBall.VelocityY = ny * power * 0.35;
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
                    double collisionDistance = radius + radius;

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
        public void CheckGameOver()
        {
            bool flag = true;
            
            foreach (Ball ball in balls)
            {
                if (ball == balls[7])
                {
                    continue;
                }
                if (!ball.fallen)
                {
                    flag = false;
                }

            }

            if (!moving) { 
            if (balls[7].fallen && balls[15].fallen && !gameOverFlag && !ball8Fallen)
            {
                ball8Fallen = true;
                MessageBox.Show("RABOTI DEKAM I CRNA I BELA SE PADNATI!!!");
                gameOverFlag = true;
                
            }
            if (balls[7].fallen && !flag && !gameOverFlag && !ball8Fallen)
            {
                ball8Fallen = true;
                MessageBox.Show("RABOTI!!!");
                gameOverFlag = true;
            }

            if (balls[7].fallen && flag && !gameOverFlag && !ball8Fallen)
            {
                ball8Fallen = true;
                MessageBox.Show("Pobedi!");
                gameOverFlag = true;
                
            }
            if (balls[15].fallen && !whiteBallJustFallen)
            {
                whiteBallJustFallen = true;
                failsCounter++;

                if (failsCounter >= 4 && !gameOverFlag)
                {
                    MessageBox.Show("Како успеа 3 пати белата топка да ја внeсеш???!");
                    gameOverFlag = true;
                    
                }
            }
            else if (!balls[15].fallen && whiteBallJustFallen)
            {
                whiteBallJustFallen = false;
            }
            }
        }
        public void DrawHeart(Graphics g, int x, int y, int size)
        {
            //Направено со ChatGPT

            Brush brush;
            GraphicsPath path = new GraphicsPath();

            if (DarkMode)
            {
                brush = new SolidBrush(Color.FromArgb(107, 104, 179));
            }
            else
            {
                brush = new SolidBrush(Color.Red);
            }

            float width = size;
            float height = size;

            // Left arc
            path.AddArc(x, y, width / 2, height / 2, 125, 225);

            // Right arc
            path.AddArc(x + width / 2, y, width / 2, height / 2, 190, 225);

            // Bottom point
            PointF bottom = new PointF(x + width / 2, y + height - 5);
            path.AddLine(path.GetLastPoint(), bottom);
            path.CloseFigure();

            g.FillPath(brush, path);
        }
        public void DrawLives(Graphics g)
        {
            if (failsCounter == 1)
            {
                DrawHeart(g, 25, 15, 50);
                DrawHeart(g, 85, 15, 50);
                DrawHeart(g, 145, 15, 50);
            }
            if (failsCounter == 2)
            {
                DrawHeart(g, 25, 15, 50);
                DrawHeart(g, 85, 15, 50);
            }
            if (failsCounter == 3)
            {
                DrawHeart(g, 25, 15, 50);
            }
        }

        public void DisplayMiniTable(Graphics g)
        {
            Brush bEdge;
            Brush bInside;

            if (DarkMode)
            {
                bEdge = new SolidBrush(Color.FromArgb(80, 77, 140));
                bInside = new SolidBrush(Color.FromArgb(48, 46, 87));
            }
            else
            {
                bEdge = new SolidBrush(Color.FromArgb(91, 52, 21));
                bInside = new SolidBrush(Color.DarkGreen);
            }

            PointF p1 = new PointF((float)(points[1].X - radius * 8 * 2 * 1.4 + 10), 670 - radius * 2);
            PointF p2 = new PointF((float)(points[1].X + 8 * radius * 2 * 1.4 - 10), 670 - radius * 2);
            PointF p3 = new PointF((float)(points[1].X + 8 * radius * 2 * 1.4 - 10), 670 + radius * 2);
            PointF p4 = new PointF((float)(points[1].X - radius * 8 * 2 * 1.4 + 10), 670 + radius * 2);

            PointF[] cornerPoints = new PointF[] { p1, p2, p3, p4 };

            PointF[] miniPoints = new PointF[] {
                new PointF(p1.X-5, p1.Y+5),
                new PointF(p1.X+5, p1.Y-5),
                new PointF(p2.X-5, p2.Y-5),
                new PointF(p2.X+5, p2.Y+5),
                new PointF(p3.X+5, p3.Y-5),
                new PointF(p3.X-5, p3.Y+5),
                new PointF(p4.X+5, p4.Y+5),
                new PointF(p4.X-5, p4.Y-5),
            };

            g.FillPolygon(bEdge, miniPoints);

            g.FillEllipse(bEdge, p1.X - 5.5f, p1.Y - 5.5f, 15, 15);
            g.FillEllipse(bEdge, p2.X - 10, p2.Y - 5.5f, 15, 15);
            g.FillEllipse(bEdge, p3.X - 10, p3.Y - 10, 15, 15);
            g.FillEllipse(bEdge, p4.X - 5.5f, p4.Y - 10, 15, 15);

            g.FillRectangle(bInside, p1.X+5, p1.Y+5, p3.X - p1.X -10, p3.Y - p1.Y -10);
        }

        public void DisplayFallen(Graphics g)
        {
            
            for(int i=0; i<15; i++)
            {
                if (balls[i].fallen)
                {
                    if (i < 7) 
                    { 
                    balls[i].ballX = points[1].X - radius*(7-i)*2*1.4 ;
                    }
                    else if(i == 7)
                    {
                        balls[i].ballX = points[1].X;
                    }
                    else
                    {
                        balls[i].ballX = points[1].X + (i-7) * radius*2*1.4;
                    }
                    balls[i].ballY = 670;
                    balls[i].VelocityX = 0;
                    balls[i].VelocityY = 0;
                }
                else
                {
                    Brush b;

                    if (DarkMode)
                    {
                        b = new SolidBrush(Color.FromArgb(61, 61, 105));
                    }
                    else
                    {
                        b = new SolidBrush(Color.Green);
                    }

                    PointF placeholder;

                    if (i < 7)
                    {
                        placeholder = new PointF((float)(points[1].X - radius * (7 - i) * 2 * 1.4), 670);
                        
                    }
                    else if (i == 7)
                    {
                        placeholder = new PointF((float)(points[1].X), 670);
                    }
                    else
                    {
                        placeholder = new PointF((float)(points[1].X + (i - 7) * radius * 2 * 1.4), 670);
                    }

                    g.FillEllipse(b, placeholder.X - radius * 0.7f, placeholder.Y - radius * 0.7f, radius * 1.4f, radius * 1.4f);

                    b.Dispose();
                }
            }
            }
        }
    }

