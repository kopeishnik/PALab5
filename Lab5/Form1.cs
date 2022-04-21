namespace Lab5
{
    public partial class Form1 : Form
    {
        bool GameIsGoingOn = false;
        bool PlayerTurn = false;
        bool PlayerIsFirst = false;
        Board GameBoard = new();

        public Form1()
        {
            InitializeComponent();
            GameIsGoingOn = false;
            MakeAllUnclickable();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAllTexts();
            MakeAllUnclickable();
            startButton.Enabled = true;
            checkBox1.Enabled = true;
            comboBox1.Enabled = true;
            richTextBox1.Text = "Press start!";
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeAllUnclickable();
            startButton.Enabled = true;
            checkBox1.Enabled = true;
            comboBox1.Enabled = true;
            richTextBox1.Text = "Press start!";
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = "Ця програма була створена Зеленським Олександром.\nLicensed under the terms of the GNU Lesser General Public License v3 or later (LGPLv3+)";
            string caption = "Про програму";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            _ = MessageBox.Show(message, caption, buttons);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Random")
            {
                Algorithms.MaxDepth = 1;
            }
            else if (comboBox1.Text == "Easy")
            {
                Algorithms.MaxDepth = 3;
            }
            else if (comboBox1.Text == "Normal")
            {
                Algorithms.MaxDepth = 5;
            }
            else if (comboBox1.Text == "Hard")
            {
                Algorithms.MaxDepth = 7;
            }
            else
            {
                MessageBox.Show("Chose hardness level first!");
                Algorithms.MaxDepth = 5;
                return;
            }
            MakeAllClickable();
            ClearAllTexts();

            startButton.Enabled = false;
            checkBox1.Enabled = false;
            comboBox1.Enabled = false;
            GameIsGoingOn = true;

            if (checkBox1.Checked) PlayerIsFirst = true; 
            else PlayerIsFirst = false;

            GameBoard = new();

            if (!PlayerIsFirst)
            {
                BotMove();
            }
            else
            {
                PlayerTurn = true;
                richTextBox1.Text = "Player move!";
            }
        }

        private void FieldButton_Click(object sender, EventArgs e)
        {
            if (!PlayerTurn)
            {
                MessageBox.Show("This is not your turn");
            }
            else
            {
                string buttonName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                var move = Processing.ReturnPlaceByButtonName(buttonName);
                GameBoard.Move(move);
                MarkMove(move, false, PlayerIsFirst);

                if (Processing.CheckForEnd(GameBoard))
                {
                    GameEnded(true);
                }
                else
                {
                    BotMove();
                }
            }
        }
        public void Log(string text)
        {
            richTextBox1.Text += text;
        }
        private void BotMove()
        {
            PlayerTurn = false;
            richTextBox1.Text = "Bot's move!";

            var move = Algorithms.AlphaBetaPruning(GameBoard, !PlayerIsFirst);
            MarkMove(move, true, PlayerIsFirst);

            if (Processing.CheckForEnd(GameBoard))
            {
                GameEnded(false);
            }
            else
            {
                richTextBox1.Text = "Your move!";
                PlayerTurn = true;
            }
        }

        private bool CheckForEnd()
        {
            return false;
        }

        private void GameEnded(bool playerWon)
        {
            MakeAllUnclickable();
            startButton.Enabled = true;
            checkBox1.Enabled = true;
            comboBox1.Enabled = true;
            richTextBox1.Text = "";
            if (playerWon)
            {
                richTextBox1.Text = "Player Won!";
            }
            else
            {
                richTextBox1.Text = "Bot Won!";
            }
        }

        private void MarkMove((int, int) move, bool isBot, bool isPlayerFirst)
        {
            Button buttonPressed = GetButtonByCoordinates(move);
            if (isBot != isPlayerFirst) buttonPressed.Text = "X";
            else buttonPressed.Text = "O";
            buttonPressed.Enabled = false;

            int x = move.Item1, y = move.Item2;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(x - i) <= 1 && Math.Abs(y - j) <= 1) GetButtonByCoordinates((i, j)).Enabled = false;
                }
            }
            return;
        }

        private Button GetButtonByCoordinates((int, int) point)
        {
            Button button = button1A;
            int key1 = point.Item1 + 1, key2 = point.Item2 + 1;
            if (key1 == 1)
            {
                if (key2 == 1) button = button1A;
                else if (key2 == 2) button = button1B;
                else if (key2 == 3) button = button1C;
                else if (key2 == 4) button = button1D;
                else if (key2 == 5) button = button1E;
                else if (key2 == 6) button = button1F;
                else if (key2 == 7) button = button1G;
                else if (key2 == 8) button = button1H;
            }
            else if (key1 == 2)
            {
                if (key2 == 1) button = button2A;
                else if (key2 == 2) button = button2B;
                else if (key2 == 3) button = button2C;
                else if (key2 == 4) button = button2D;
                else if (key2 == 5) button = button2E;
                else if (key2 == 6) button = button2F;
                else if (key2 == 7) button = button2G;
                else if (key2 == 8) button = button2H;
            }
            else if (key1 == 3)
            {
                if (key2 == 1) button = button3A;
                else if (key2 == 2) button = button3B;
                else if (key2 == 3) button = button3C;
                else if (key2 == 4) button = button3D;
                else if (key2 == 5) button = button3E;
                else if (key2 == 6) button = button3F;
                else if (key2 == 7) button = button3G;
                else if (key2 == 8) button = button3H;
            }
            else if (key1 == 4)
            {
                if (key2 == 1) button = button4A;
                else if (key2 == 2) button = button4B;
                else if (key2 == 3) button = button4C;
                else if (key2 == 4) button = button4D;
                else if (key2 == 5) button = button4E;
                else if (key2 == 6) button = button4F;
                else if (key2 == 7) button = button4G;
                else if (key2 == 8) button = button4H;
            }
            else if (key1 == 5)
            {
                if (key2 == 1) button = button5A;
                else if (key2 == 2) button = button5B;
                else if (key2 == 3) button = button5C;
                else if (key2 == 4) button = button5D;
                else if (key2 == 5) button = button5E;
                else if (key2 == 6) button = button5F;
                else if (key2 == 7) button = button5G;
                else if (key2 == 8) button = button5H;
            }
            else if (key1 == 6)
            {
                if (key2 == 1) button = button6A;
                else if (key2 == 2) button = button6B;
                else if (key2 == 3) button = button6C;
                else if (key2 == 4) button = button6D;
                else if (key2 == 5) button = button6E;
                else if (key2 == 6) button = button6F;
                else if (key2 == 7) button = button6G;
                else if (key2 == 8) button = button6H;
            }
            else if (key1 == 7)
            {
                if (key2 == 1) button = button7A;
                else if (key2 == 2) button = button7B;
                else if (key2 == 3) button = button7C;
                else if (key2 == 4) button = button7D;
                else if (key2 == 5) button = button7E;
                else if (key2 == 6) button = button7F;
                else if (key2 == 7) button = button7G;
                else if (key2 == 8) button = button7H;
            }
            else
            {
                if (key2 == 1) button = button8A;
                else if (key2 == 2) button = button8B;
                else if (key2 == 3) button = button8C;
                else if (key2 == 4) button = button8D;
                else if (key2 == 5) button = button8E;
                else if (key2 == 6) button = button8F;
                else if (key2 == 7) button = button8G;
                else button = button8H;
            }
            return button;
        }

        private Button GetButtonByName(string name)
        {
            Button button = button1A;
            int key1 = 0;
            if (name[6] == '1')
            {
                key1 = 1;
            }
            else if (name[6] == '2')
            {
                key1 = 2;
            }
            else if (name[6] == '3')
            {
                key1 = 3;
            }
            else if (name[6] == '4')
            {
                key1 = 4;
            }
            else if (name[6] == '5')
            {
                key1 = 5;
            }
            else if (name[6] == '6')
            {
                key1 = 6;
            }
            else if (name[6] == '7')
            {
                key1 = 7;
            }
            else if (name[6] == '8')
            {
                key1 = 8;
            }
            if (key1 == 1)
            {
                if (name[7] == 'A') button = button1A;
                else if (name[7] == 'B') button = button1B;
                else if (name[7] == 'C') button = button1C;
                else if (name[7] == 'D') button = button1D;
                else if (name[7] == 'E') button = button1E;
                else if (name[7] == 'F') button = button1F;
                else if (name[7] == 'G') button = button1G;
                else if (name[7] == 'H') button = button1H;
            }
            else if (key1 == 2)
            {
                if (name[7] == 'A') button = button2A;
                else if (name[7] == 'B') button = button2B;
                else if (name[7] == 'C') button = button2C;
                else if (name[7] == 'D') button = button2D;
                else if (name[7] == 'E') button = button2E;
                else if (name[7] == 'F') button = button2F;
                else if (name[7] == 'G') button = button2G;
                else if (name[7] == 'H') button = button2H;
            } 
            else if (key1 == 3)
            {
                if (name[7] == 'A') button = button3A;
                else if (name[7] == 'B') button = button3B;
                else if (name[7] == 'C') button = button3C;
                else if (name[7] == 'D') button = button3D;
                else if (name[7] == 'E') button = button3E;
                else if (name[7] == 'F') button = button3F;
                else if (name[7] == 'G') button = button3G;
                else if (name[7] == 'H') button = button3H;
            }
            else if (key1 == 4)
            {
                if (name[7] == 'A') button = button4A;
                else if (name[7] == 'B') button = button4B;
                else if (name[7] == 'C') button = button4C;
                else if (name[7] == 'D') button = button4D;
                else if (name[7] == 'E') button = button4E;
                else if (name[7] == 'F') button = button4F;
                else if (name[7] == 'G') button = button4G;
                else if (name[7] == 'H') button = button4H;
            }
            else if (key1 == 5)
            {
                if (name[7] == 'A') button = button5A;
                else if (name[7] == 'B') button = button5B;
                else if (name[7] == 'C') button = button5C;
                else if (name[7] == 'D') button = button5D;
                else if (name[7] == 'E') button = button5E;
                else if (name[7] == 'F') button = button5F;
                else if (name[7] == 'G') button = button5G;
                else if (name[7] == 'H') button = button5H;
            }
            else if (key1 == 6)
            {
                if (name[7] == 'A') button = button6A;
                else if (name[7] == 'B') button = button6B;
                else if (name[7] == 'C') button = button6C;
                else if (name[7] == 'D') button = button6D;
                else if (name[7] == 'E') button = button6E;
                else if (name[7] == 'F') button = button6F;
                else if (name[7] == 'G') button = button6G;
                else if (name[7] == 'H') button = button6H;
            }
            else if (key1 == 7)
            {
                if (name[7] == 'A') button = button7A;
                else if (name[7] == 'B') button = button7B;
                else if (name[7] == 'C') button = button7C;
                else if (name[7] == 'D') button = button7D;
                else if (name[7] == 'E') button = button7E;
                else if (name[7] == 'F') button = button7F;
                else if (name[7] == 'G') button = button7G;
                else if (name[7] == 'H') button = button7H;
            }
            else
            {
                if (name[7] == 'A') button = button8A;
                else if (name[7] == 'B') button = button8B;
                else if (name[7] == 'C') button = button8C;
                else if (name[7] == 'D') button = button8D;
                else if (name[7] == 'E') button = button8E;
                else if (name[7] == 'F') button = button8F;
                else if (name[7] == 'G') button = button8G;
                else button = button8H;
            }
            return button;
        }

        private void MakeAllUnclickable()
        {
            button1A.Enabled = false;
            button1B.Enabled = false;
            button1C.Enabled = false;
            button1D.Enabled = false;
            button1E.Enabled = false;
            button1F.Enabled = false;
            button1G.Enabled = false;
            button1H.Enabled = false;

            button2A.Enabled = false;
            button2B.Enabled = false;
            button2C.Enabled = false;
            button2D.Enabled = false;
            button2E.Enabled = false;
            button2F.Enabled = false;
            button2G.Enabled = false;
            button2H.Enabled = false;

            button3A.Enabled = false;
            button3B.Enabled = false;
            button3C.Enabled = false;
            button3D.Enabled = false;
            button3E.Enabled = false;
            button3F.Enabled = false;
            button3G.Enabled = false;
            button3H.Enabled = false;

            button4A.Enabled = false;
            button4B.Enabled = false;
            button4C.Enabled = false;
            button4D.Enabled = false;
            button4E.Enabled = false;
            button4F.Enabled = false;
            button4G.Enabled = false;
            button4H.Enabled = false;

            button5A.Enabled = false;
            button5B.Enabled = false;
            button5C.Enabled = false;
            button5D.Enabled = false;
            button5E.Enabled = false;
            button5F.Enabled = false;
            button5G.Enabled = false;
            button5H.Enabled = false;

            button6A.Enabled = false;
            button6B.Enabled = false;
            button6C.Enabled = false;
            button6D.Enabled = false;
            button6E.Enabled = false;
            button6F.Enabled = false;
            button6G.Enabled = false;
            button6H.Enabled = false;

            button7A.Enabled = false;
            button7B.Enabled = false;
            button7C.Enabled = false;
            button7D.Enabled = false;
            button7E.Enabled = false;
            button7F.Enabled = false;
            button7G.Enabled = false;
            button7H.Enabled = false;

            button8A.Enabled = false;
            button8B.Enabled = false;
            button8C.Enabled = false;
            button8D.Enabled = false;
            button8E.Enabled = false;
            button8F.Enabled = false;
            button8G.Enabled = false;
            button8H.Enabled = false;
        }

        private void MakeAllClickable()
        {
            button1A.Enabled = true;
            button1B.Enabled = true;
            button1C.Enabled = true;
            button1D.Enabled = true;
            button1E.Enabled = true;
            button1F.Enabled = true;
            button1G.Enabled = true;
            button1H.Enabled = true;

            button2A.Enabled = true;
            button2B.Enabled = true;
            button2C.Enabled = true;
            button2D.Enabled = true;
            button2E.Enabled = true;
            button2F.Enabled = true;
            button2G.Enabled = true;
            button2H.Enabled = true;

            button3A.Enabled = true;
            button3B.Enabled = true;
            button3C.Enabled = true;
            button3D.Enabled = true;
            button3E.Enabled = true;
            button3F.Enabled = true;
            button3G.Enabled = true;
            button3H.Enabled = true;

            button4A.Enabled = true;
            button4B.Enabled = true;
            button4C.Enabled = true;
            button4D.Enabled = true;
            button4E.Enabled = true;
            button4F.Enabled = true;
            button4G.Enabled = true;
            button4H.Enabled = true;

            button5A.Enabled = true;
            button5B.Enabled = true;
            button5C.Enabled = true;
            button5D.Enabled = true;
            button5E.Enabled = true;
            button5F.Enabled = true;
            button5G.Enabled = true;
            button5H.Enabled = true;

            button6A.Enabled = true;
            button6B.Enabled = true;
            button6C.Enabled = true;
            button6D.Enabled = true;
            button6E.Enabled = true;
            button6F.Enabled = true;
            button6G.Enabled = true;
            button6H.Enabled = true;

            button7A.Enabled = true;
            button7B.Enabled = true;
            button7C.Enabled = true;
            button7D.Enabled = true;
            button7E.Enabled = true;
            button7F.Enabled = true;
            button7G.Enabled = true;
            button7H.Enabled = true;

            button8A.Enabled = true;
            button8B.Enabled = true;
            button8C.Enabled = true;
            button8D.Enabled = true;
            button8E.Enabled = true;
            button8F.Enabled = true;
            button8G.Enabled = true;
            button8H.Enabled = true;
        }
        private void ClearAllTexts()
        {
            button1A.Text = "";
            button1B.Text = "";
            button1C.Text = "";
            button1D.Text = "";
            button1E.Text = "";
            button1F.Text = "";
            button1G.Text = "";
            button1H.Text = "";
            button2A.Text = "";
            button2B.Text = "";
            button2C.Text = "";
            button2D.Text = "";
            button2E.Text = "";
            button2F.Text = "";
            button2G.Text = "";
            button2H.Text = "";
            button3A.Text = "";
            button3B.Text = "";
            button3C.Text = "";
            button3D.Text = "";
            button3E.Text = "";
            button3F.Text = "";
            button3G.Text = "";
            button3H.Text = "";
            button4A.Text = "";
            button4B.Text = "";
            button4C.Text = "";
            button4D.Text = "";
            button4E.Text = "";
            button4F.Text = "";
            button4G.Text = "";
            button4H.Text = "";
            button5A.Text = "";
            button5B.Text = "";
            button5C.Text = "";
            button5D.Text = "";
            button5E.Text = "";
            button5F.Text = "";
            button5G.Text = "";
            button5H.Text = "";
            button6A.Text = "";
            button6B.Text = "";
            button6C.Text = "";
            button6D.Text = "";
            button6E.Text = "";
            button6F.Text = "";
            button6G.Text = "";
            button6H.Text = "";
            button7A.Text = "";
            button7B.Text = "";
            button7C.Text = "";
            button7D.Text = "";
            button7E.Text = "";
            button7F.Text = "";
            button7G.Text = "";
            button7H.Text = "";
            button8A.Text = "";
            button8B.Text = "";
            button8C.Text = "";
            button8D.Text = "";
            button8E.Text = "";
            button8F.Text = "";
            button8G.Text = "";
            button8H.Text = "";
        }
    }
}