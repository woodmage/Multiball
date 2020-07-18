using System;
using System.Drawing;
using System.Windows.Forms;

public class Extra
{
    Timer timer = new Timer();
    Image image;
    //Image[] images = new Image[8];
    public int x, y, w, h, n;
    private bool ishit;
    private Random rand = new Random();
    public void Set(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        ishit = false;
//        image = Image.FromFile("");
        //n = 0;
        //for (int i = 0; i < 8; i++)
        //{
            //images[i] = Image.FromFile($"D:\\work\\games\\work\\extra{i + 1}.png");
        //}
        //timer.Tick += new EventHandler(newn);
        //timer.Interval = 50;
        //timer.Start();
    }
    public void SetImage(Image i)
    {
        image = i;
    }
    private void newn(object sender, EventArgs e)
    {
        n++;
        if (n > 7) n = 0;
    }
    //public void Kill()
    //{
        //timer.Stop();
        //timer.Dispose();
    //}
    private bool Check(int x,int y,int w,int h)
    {
        if (x > this.x + this.w - 1) return false;
        if (x + w - 1 < this.x) return false;
        if (y > this.y + this.h - 1) return false;
        if (y + h - 1 < this.y) return false;
        return true;
    }
    public void Drop()
    {
        y += h;
    }
    public bool Ball(Ball ball)
    {
        ball.Move();
        if (Check((int)ball.x, (int)ball.y, (int)ball.w, (int)ball.h))
        {
            ishit = true;
            return true;
        }
        return false;
    }
    public bool IsHit()
    {
        return ishit;
    }
    public void Paint(PaintEventArgs e)
    {
        if (!ishit)
        {
            //Image image;
            e.Graphics.DrawImage(image, x, y, 50, 50);

            //e.Graphics.DrawImage(images[n], x, y, 50, 50);
        }
    }
}