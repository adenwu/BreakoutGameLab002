using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BreakoutGameLab001
{
    internal class BrickGamePanel : Panel
    {
        // 定義遊戲元件
        private List<Ball> balls = new List<Ball>();
        private Paddle paddle;
        private List<Brick> bricks = new List<Brick>();
        // 定義 Timer 控制項
        private Timer timer = new Timer();

        public BrickGamePanel(int width, int height)
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Yellow;
            this.Size = new Size(width, height);
        }

        public void Initialize()
        {
            // 初始化遊戲元件
            balls.Add(new Ball(Width / 2, Height / 2, 15, 3, -3, Color.Red));
            balls.Add(new Ball(Width / 3, Height / 2, 15, -3, 3, Color.Green));
            balls.Add(new Ball(Width / 2, Height / 3, 15, 3, 3, Color.Blue));

            paddle = new Paddle(Width / 2 - 50, Height - 50, 120, 20, Color.Blue);

            // 初始化磚塊
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bricks.Add(new Brick(25 + j * 80, 25 + i * 30, 80, 30, Color.Green));
                }
            }

            // 設定遊戲的背景控制流程: 每 20 毫秒觸發一次 Timer_Tick 事件 ==> 更新遊戲畫面
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 定時移動球的位置, 檢查碰撞事件
                foreach (Ball ball in balls)
                {
                    ball.Move(0, 0, Width, Height);
                    ball.CheckCollision(bricks);
                }

                CheckBallCollisions();
                CheckGameOver();

                // 重繪遊戲畫面
                Invalidate(); // --> 觸發 OnPaint 事件
            }
            catch (GameOverException ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message, "Game Over");
                Application.Exit();
            }
        }

        private void CheckBallCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    balls[i].CheckCollision(balls[j]);
                }
            }
        }

        private void CheckGameOver()
        {
            if (bricks.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Congratulations! You cleared all the bricks!", "Game Over");
                Application.Exit();
            }
        }

        // 處理畫面的重繪事件
        protected override void OnPaint(PaintEventArgs e)
        {
            // 呼叫基底類別的 OnPaint 方法 --> 這樣才能正確繪製 Panel 控制項
            base.OnPaint(e);

            // 取得 Graphics 物件
            Graphics gr = e.Graphics;

            // 繪製球
            foreach (Ball ball in balls)
            {
                ball.Draw(gr);
            }

            // 繪製擋板
            paddle.Draw(gr);

            // 繪製磚塊
            foreach (var brick in bricks)
            {
                brick.Draw(gr);
            }
        }

        public void PaddleMoveLeft()
        {
            paddle.Move(-30);
        }

        public void PaddleMoveRight()
        {
            paddle.Move(30);
        }
    }
}
