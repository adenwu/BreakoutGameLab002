using System.Drawing;
using System.Collections.Generic;

namespace BreakoutGameLab001
{
    // 球類別
    class Ball
    {
        // 屬性
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }

        // 建構子
        public Ball(int x, int y, int radius, int vx, int vy, Color color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Color = color;
            VelocityX = vx;
            VelocityY = vy;
        }

        // 繪製球
        internal void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(this.Color), X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }

        // 移動球
        public void Move(int left, int top, int right, int bottom)
        {
            X += VelocityX;
            Y += VelocityY;

            // 水平方向: 檢查球是否碰到牆壁
            if (X - Radius <= left)
            {
                VelocityX = -VelocityX; // 球反彈
                X = left + Radius; // 避免球超出邊界
            }
            else if (X + Radius >= right)
            {
                VelocityX = -VelocityX; // 球反彈
                X = right - Radius; // 避免球超出邊界
            }

            // 垂直方向: 檢查球是否碰到牆壁
            if (Y - Radius <= top)
            {
                VelocityY = -VelocityY;
                Y = top + Radius;
            }
            else if (Y + Radius >= bottom)
            {
                // 這裡返回 false 以表示球碰到了底部，遊戲結束
                Y = bottom - Radius;
                throw new GameOverException("Ball missed the paddle!");
            }
        }

        // 檢查球與磚塊之間的碰撞
        public void CheckCollision(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                Brick brick = bricks[i];
                if (brick != null)
                {
                    if (X + Radius >= brick.X && X - Radius <= brick.X + brick.Width &&
                        Y + Radius >= brick.Y && Y - Radius <= brick.Y + brick.Height)
                    {
                        VelocityY = -VelocityY; // 球反彈
                        bricks.RemoveAt(i); // 移除磚塊
                        i--; // 調整索引以避免跳過下一個磚塊
                    }
                }
            }
        }

        // 檢查球與球之間的碰撞
        public void CheckCollision(Ball other)
        {
            int dx = other.X - X;
            int dy = other.Y - Y;
            int distance = (int)Math.Sqrt(dx * dx + dy * dy);

            if (distance <= Radius + other.Radius)
            {
                // 交換速度
                int tempVx = VelocityX;
                int tempVy = VelocityY;
                VelocityX = other.VelocityX;
                VelocityY = other.VelocityY;
                other.VelocityX = tempVx;
                other.VelocityY = tempVy;
            }
        }
    }
}
