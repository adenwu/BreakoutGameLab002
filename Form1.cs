namespace BreakoutGameLab001
{
    public partial class Form1 : Form
    {
        private BrickGamePanel gamePanel;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Controls.Remove(panel2);
            gamePanel = new BrickGamePanel(panel2.Width, panel2.Height);
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.Location = new Point(0, 61);
            gamePanel.Name = "BrickoutGamePanel";
            gamePanel.Size = new Size(panel2.Width, panel2.Height);
            gamePanel.TabIndex = 1;
            gamePanel.Initialize();
            Controls.Add(gamePanel);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gamePanel.PaddleMoveLeft();
                    break;
                case Keys.Right:
                    gamePanel.PaddleMoveRight();
                    break;
            }
        }
    }
}
