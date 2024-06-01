namespace BreakoutGameLab001
{
    // 擋板類別
    class Paddle
    {
        // 屬性
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        // 建構子
        public Paddle(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
        }

        // 加入其他方法

        // 繪製擋板
        internal void Draw(Graphics gr)
        {
            gr.FillRectangle(new SolidBrush(Color.Blue), X, Y, Width, Height);
        }

        // TODO: 左右移動擋板
        public void Move(int vx)
        {
            X += vx;
        }

    }
}