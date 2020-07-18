using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class Block
{
    public int x, y, w, h, nh, points;
    public Color color;
    private bool ishit;
    public void Set(int x, int y, int w, int h, int nh, Color color, int points)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.nh = nh;
        this.color = color;
        this.points = points;
    }
    public bool Ball(Ball ball)
    {
        ishit = false;
        ball.MoveX();
        if (IsHit(ball))
        {
            ball.HitX();
            switch(HitWhereX(ball))
            {
                //case -1:
                    //ball.dy -= 15;
                    //break;
                case 1:
                    ball.dy += 15;
                    break;
            }
            ishit = true;
            nh--;
        }
        ball.MoveY();
        if (IsHit(ball))
        {
            ball.HitY();
            ishit = true;
            nh--;
        }
        return ishit;
    }
    public void Drop() => y += h;
    public bool Exists() => (nh > 0);
    public int HitWhereX(int x, int y, int w, int h)
    {
        int ret;
        ret = 0;
        if (y + h - 1 < this.y + (this.h * 2 / 3 - 1)) ret = -1;
        if (y > this.y + (this.h / 3 - 1)) ret = 1;
        return ret;
    }
    public int HitWhereX(float x, float y, float w, float h) => (HitWhereX((int)x, (int)y, (int)w, (int)h));
    public int HitWhereX(Ball ball) => (HitWhereX(ball.x, ball.y, ball.w, ball.h));
    public bool IsHit(int x, int y, int w, int h)
    {
        if (x > this.x + this.w - 1) return false;
        if (x + w - 1 < this.x) return false;
        if (y > this.y + this.h - 1) return false;
        if (y + h - 1 < this.y) return false;
        return true;
    }
    public bool IsHit(float x, float y, float w, float h)
    {
        return IsHit((int)x, (int)y, (int)w, (int)h);
    }
    public bool IsHit(Ball ball) => IsHit(ball.x, ball.y, ball.w, ball.h);
    public void Paint(PaintEventArgs e)
    {
        HatchBrush brush = new HatchBrush(HatchStyle.DottedDiamond, color);
        e.Graphics.FillRectangle(brush, x, y, w, h);
        Pen pen = new Pen(color);
        e.Graphics.DrawRectangle(pen, x, y, w, h);
        SolidBrush solid = new SolidBrush(Color.White);
        RectangleF rect = new RectangleF(x, y, w, h);
        Font font = new Font("Ink Free", 9, FontStyle.Bold);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        e.Graphics.DrawString($"{nh}", font, solid, rect, format);
    }
}
