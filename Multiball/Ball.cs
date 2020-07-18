using System;
using System.Drawing;
using System.Windows.Forms;

public class Ball
{
    public float x, y, w, h, dx, dy;
    public float x1, y1, x2, y2;
    public bool ishit;
    private Random rand = new Random();
    public void Set(float x, float y, float w, float h, float dx, float dy)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.dx = dx;
        this.dy = dy;
        ishit = false;
    }
    public void Set(Ball ball) => Set(ball.x, ball.y, ball.w, ball.h, ball.dx, ball.dy);
    public void Bounds(float x1, float y1, float x2, float y2)
    {
        this.x1 = x1;
        this.y1 = y1;
        this.x2 = x2;
        this.y2 = y2;
    }
    public void Bounds(Ball ball) => Bounds(ball.x1, ball.y1, ball.x2, ball.y2);
    public void Copy(Ball ball)
    {
        Set(ball);
        Bounds(ball);
    }
    public void MoveX()
    {
        x += dx;
        if (x < x1) BounceX();
        if (x > x2 - w) BounceX();
    }
    public void MoveY()
    {
        y += dy;
        if (y < y1) BounceY();
        if (y > y2 - h) BounceY();
    }
    public void Move()
    {
        MoveX();
        MoveY();
    }
    public void BounceX()
    {
        dx = -dx;
        x += dx;
    }
    public void BounceY()
    {
        dy = -dy;
        y += dy;
    }
    public void HitX()
    {
        BounceX();
        ishit = true;
        if (rand.Next(0, 100) < 5)
            dx += (float)(15.0 * rand.NextDouble() - 7.5);
    }
    public void HitY()
    {
        BounceY();
        ishit = true;
        if (rand.Next(0, 100) < 5)
            dy += (float)(15.0 * rand.NextDouble() - 7.5);
    }
    public bool IsHit() => ishit;
    public void Paint(PaintEventArgs e)
    {
        SolidBrush brush = new SolidBrush(Color.White);
        e.Graphics.FillEllipse(brush, x, y, w, h);
    }
}
