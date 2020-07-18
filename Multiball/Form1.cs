using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiball
{
    public partial class MultiBall : Form
    {
        static int maxballs = 999;
        static int maxblocks = 200;
        static int maxextras = 20;
        private Timer timer = new Timer();
        private Timer extratimer = new Timer();
        private Ball[] balls = new Ball[maxballs];
        private bool[] isBall = new bool[maxballs];
        private int numballs, ballno, numberballs;
        private Ball ball = new Ball();
        private Block[] blocks = new Block[maxblocks];
        private bool[] isBlock = new bool[maxblocks];
        private int numblocks;
        private Extra[] extras = new Extra[maxextras];
        private bool[] isExtra = new bool[maxextras];
        private int numextras;
        private int level, score;
        private Random rand = new Random();
        private Color[] colors = { Color.Blue, Color.Green, Color.YellowGreen, Color.Yellow, Color.Orange, Color.OrangeRed, Color.Red };
        private bool aimmode;
        private bool speedmode;
        private bool pausemode;
        private bool supermode;
        private bool infinitemode;
        private bool cheatmode;
        private bool busymode;
        private float x1, y1, x2, y2, dx, dy;
        public Image image;
        private Image[] images = new Image[8];
        private int imagen = 0;
        public MultiBall()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerProc);
            timer.Interval = 100;
            InitGame();
            timer.Start();
            extratimer.Tick += new EventHandler(ExtraProc);
            extratimer.Interval = 50;
            extratimer.Start();
            image = Image.FromFile(@"D:\work\games\work\extra1.png");
            for (int i = 0; i < 8; i++)
            {
                images[i] = Image.FromFile($"D:\\work\\games\\work\\extra{i + 1}.png");
            }
        }
        private void InitGame()
        {
            score = 0;
            cheatmode = false;
            supermode = false;
            infinitemode = false;
            numberballs = 1;
            level = 1;
            numblocks = 0;
            numextras = 0;
            for (int i = 0; i < maxblocks; i++)
                isBlock[i] = false;
            for (int i = 0; i < maxextras; i++)
                isExtra[i] = false;
            for (int i = 0; i < maxballs; i++)
                isBall[i] = false;
            InitLevel();
            InitLevel();
        }
        private void InitLevel()
        {
            int nb;
            bool go = false;
            for (int i = 0; i < maxballs; i++)
                isBall[i] = false;
            for (int i = 0; i < maxblocks; i++)
            {
                if (isBlock[i])
                {
                    blocks[i].Drop();
                    if (blocks[i].y >= 500)
                        go = true;
                }
            }
            for (int i = 0; i < maxextras; i++)
            {
                if (isExtra[i])
                    extras[i].Drop();
            }
            nb = rand.Next(1, 7);
            for (int i = 0; i < nb; i++)
            {
                AddBlock();
            }
            if (level > 1)
                AddExtra();
            aimmode = true;
            x1 = 200;
            y1 = 500;
            numballs = numberballs;
            ballno = 0;
            speedmode = false;
            timer.Stop();
            timer.Interval = 100;
            timer.Enabled = true;
            pausemode = false;
            busymode = false;
            if (go)
            {
                busymode = true;
                if (MessageBox.Show($"You made {score} in {level} levels.  Play again?", "You died!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    InitGame();
                else
                    Application.Exit();
            }
        }
        private void AddBlock()
        {
            int x, nh, r, p;
            Color color;
            Block block = new Block();
            do
            {
                nh = level / 2;
                color = colors[3];
                r = rand.Next(0, 100);
                if (r < 1) //1%
                {
                    if (rand.Next(0, 100) < 50)
                    {
                        nh -= 3;
                        color = colors[0];
                    }
                    else
                    {
                        nh += 3;
                        color = colors[6];
                    }
                }
                else if (r < 5) //4%
                {
                    if (rand.Next(0, 100) < 50)
                    {
                        nh -= 2;
                        color = colors[1];
                    }
                    else
                    {
                        nh += 2;
                        color = colors[5];
                    }
                }
                else if (r < 15) //10%
                {
                    if (rand.Next(0, 100) < 50)
                    {
                        nh -= 1;
                        color = colors[2];
                    }
                    else
                    {
                        nh += 1;
                        color = colors[4];
                    }
                }
                if (nh < 0) nh = 0;
                nh++;
                p = nh * 100;
                x = rand.Next(0, 8) * 50;
                block.Set(x, 0, 50, 50, nh, color, p);
            } while (TryBlock(block));
            for (int i = 0; i < maxblocks; i++)
            {
                if (!isBlock[i])
                {
                    blocks[i] = block;
                    isBlock[i] = true;
                    numblocks++;
                    i = maxblocks;
                }
            }
        }
        private bool TryBlock(Block block)
        {
            bool ret = false;
            for (int i = 0; i < maxblocks; i++)
            {
                if (isBlock[i])
                {
                    if (blocks[i].IsHit(block.x, block.y, block.w, block.h))
                    {
                        ret = true;
                        i = maxblocks;
                    }
                }
            }
            return ret;
        }
        private void AddExtra()
        {
            Extra extra = new Extra();
            int x;
            do
            {
                x = rand.Next(0, 8) * 50;
                extra.Set(x, 0, 50, 50);
                extra.SetImage(image);
            } while (TryExtra(extra));
            for (int i = 0; i < maxextras; i++)
            {
                if (!isExtra[i])
                {
                    extras[i] = extra;
                    numextras++;
                    isExtra[i] = true;
                    i = maxextras;
                }
            }
        }
        private bool TryExtra(Extra extra)
        {
            bool ret = false;
            for (int i = 0; i < maxblocks; i++)
            {
                if (isBlock[i])
                {
                    if (blocks[i].IsHit(extra.x, extra.y, extra.w, extra.h))
                    {
                        ret = true;
                        i = maxblocks;
                    }
                }
            }
            return ret;
        }
        private void Inval(int n)
        {
            switch (n)
            {
                case 1:
                    scoreArea.Invalidate();
                    break;
                case 2:
                    gameArea.Invalidate();
                    break;
                default:
                    Invalidate();
                    scoreArea.Invalidate();
                    gameArea.Invalidate();
                    break;
            }
        }
        private void Inval() => Inval(0);
        private void PaintScoreArea(object sender, PaintEventArgs e)
        {
            string b1 = $"Score: {score}   Level: {level}    Balls: {numballs}";
            string b2 = "";
            if (aimmode)
                b2 = "Move mouse to aim, press button to fire";
            else
            {
                if (speedmode)
                    b2 = "Press left mouse button to slow down, right mouse button to return all balls.";
                else
                    b2 = "Press left mouse button to speed up, right mouse button to return all balls";
            }
            string b3 = "";
            if (cheatmode)
            {
                b3 = "CHEATS!    ";
                if (supermode)
                    b3 += "Super    ";
                if (infinitemode)
                    b3 += "Infinite    ";
            }
            Font font = new Font("Ink Free", 12, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(Color.White);
            RectangleF rect = new RectangleF(0, 0, 400, 33);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(b1, font, brush, rect, format);
            rect.Y += 33;
            e.Graphics.DrawString(b2, font, brush, rect, format);
            rect.Y += 33;
            brush = new SolidBrush(Color.Red);
            e.Graphics.DrawString(b3, font, brush, rect, format);
        }
        private void PaintGameArea(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < maxblocks; i++)
            {
                if (isBlock[i])
                    blocks[i].Paint(e);
            }
            for (int i = 0; i < maxextras; i++)
            {
                if (isExtra[i])
                    extras[i].Paint(e);
            }
            SolidBrush brush = new SolidBrush(Color.Yellow);
            e.Graphics.FillEllipse(brush, x1, y1, 15, 15);
            if (aimmode)
            {
                e.Graphics.FillEllipse(brush, x2, y2, 15, 15);
                Pen pen = new Pen(brush, 5);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                e.Graphics.DrawLine(pen, x1 + 6, y1 + 6, x2 + 6, y2 + 6);
            }
            else
            {
                for (int i = 0; i < maxballs; i++)
                {
                    if (isBall[i])
                        balls[i].Paint(e);
                }
            }
            Font font = new Font("Ink Free", 9, FontStyle.Italic);
            e.Graphics.DrawString($"{numballs}", font, brush, x1, y1 + 20);
            if (pausemode)
            {
                brush = new SolidBrush(Color.Aqua);
                font = new Font("Ink Free", 60, FontStyle.Bold);
                RectangleF rect = new RectangleF(0, 0, 400, 550);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("PAUSED\n\nPress \"P\" to unpause.", font, brush, rect, format);
            }
        }
        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            if (aimmode)
            {
                x2 = e.X;
                y2 = e.Y;
                if (y2 + 15 > y1) y2 = y1 - 15;
                Inval();
            }
        }
        private void DoMouseClick(object sender, MouseEventArgs e)
        {
            if (aimmode)
            {
                double angle = Math.Atan2(x2 - x1, y2 - y1);
                dx = (float)Math.Sin(angle)*15;
                dy = (float)Math.Cos(angle)*15;
                aimmode = false;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    timer.Stop();
                    if (speedmode)
                    {
                        timer.Interval = 100;
                        speedmode = false;
                    }
                    else
                    {
                        timer.Interval = 1;
                        speedmode = true;
                    }
                    timer.Enabled = true;
                }
                if (e.Button == MouseButtons.Right)
                {
                    for (int i = 0; i < maxballs; i++)
                    {
                        if (isBall[i])
                            isBall[i] = false;
                    }
                    numballs = 0;
                    ballno = 0;
                }
            }
        }
        private void DoKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.P:
                    pausemode = !pausemode;
                    Inval();
                    break;
                case Keys.Escape:
                    busymode = true;
                    if (MessageBox.Show("Are you sure?", "Quit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Application.Exit();
                    busymode = false;
                    break;
                case Keys.S:
                    cheatmode = true;
                    supermode = !supermode;
                    Inval(1);
                    break;
                case Keys.I:
                    cheatmode = true;
                    infinitemode = !infinitemode;
                    Inval(1);
                    break;
                case Keys.B:
                    cheatmode = true;
                    for (int i = 0; i < maxblocks; i++)
                    {
                        if (isBlock[i])
                        {
                            score += blocks[i].nh;
                            score += blocks[i].points;
                            isBlock[i] = false;
                        }
                    }
                    Inval();
                    break;
            }
        }        
        private void TimerProc(object sender, EventArgs e)
        { 
            if (aimmode) return;
            if (pausemode) return;
            if (busymode) return;
            bool useball;
            ball = new Ball();
            if (ballno < numballs)
                AddBall();
            for (int i = 0; i < maxballs; i++)
            {
                if (isBall[i])
                {
                    useball = false;
                    for (int j = 0; j < maxblocks; j++)
                    {
                        if (isBlock[j])
                        {
                            ball.Copy(balls[i]);
                            if (blocks[j].Ball(balls[i]))
                            {
                                score++;
                                if (!blocks[j].Exists())
                                {
                                    isBlock[j] = false;
                                    score += blocks[j].points;
                                }
                                else if (supermode)
                                {
                                    isBlock[j] = false;
                                    score += blocks[j].nh;
                                    score += blocks[j].points;
                                }
                                useball = true;
                                Inval();
                            }
                            if (!useball) balls[i].Copy(ball);
                        }
                    }
                    if (!useball)
                    {
                        balls[i].Move();
                    }
                    for (int k = 0; k < maxextras; k++)
                    {
                        if (isExtra[k])
                        {
                            if (extras[k].Ball(balls[i]))
                            {
                                numberballs++;
                                isExtra[k] = false;
                                Inval();
                            }
                        }
                    }
                    Inval();
                    if ((balls[i].x < balls[i].x1) || (balls[i].x > balls[i].x2) || (balls[i].y < balls[i].y1) || (balls[i].y > 550))
                    {
                        isBall[i] = false;
                        ballno--;
                        numballs--;
                        if (infinitemode)
                        {
                            if (balls[i].IsHit())
                            {
                                numballs++;
                                AddBall();
                            }
                        }
                    }
                }
            }
            Inval();
            if (ballno < 1)
            {
                level++;
                InitLevel();
            }
        }
        private void AddBall()
        {
            Ball ball = new Ball();
            ball.Set(x1, y1, 15, 15, dx, dy);
            ball.Bounds(0, 0, 400, 9999);
            for (int i = 0; i < maxballs; i++)
            {
                if (!isBall[i])
                {
                    balls[i] = ball;
                    ballno++;
                    isBall[i] = true;
                    i = maxballs;
                }
            }
        }
        private void ExtraProc(object sender, EventArgs e)
        {
            imagen++;
            if (imagen > 7) imagen = 0;
            image = images[imagen];
                //Image.FromFile($"D:\\work\\game\\work\\extra{imagen + 1}.png");
            for (int i = 0; i < maxextras; i++)
            {
                if (isExtra[i])
                {
                    extras[i].SetImage(image);
                }
            }
            gameArea.Invalidate();
        }
    }
}
