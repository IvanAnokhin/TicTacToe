using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class TicTacToe : Form
    {
        private const int BoardSize = 3;
        private Button[,] buttons;
        private char[,] board;
        private char currentPlayer;

        public TicTacToe()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[BoardSize, BoardSize];
            board = new char[BoardSize, BoardSize];
            currentPlayer = 'X';

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new System.Drawing.Size(100, 100);
                    buttons[i, j].Location = new System.Drawing.Point(100 * i, 100 * j);
                    buttons[i, j].Font = new System.Drawing.Font("Arial", 40);
                    buttons[i, j].Click += new EventHandler(Button_Click);
                    Controls.Add(buttons[i, j]);
                }
            }

            ClearBoard();
            UpdateButtons();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int row = -1, col = -1;

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }

            if (row != -1 && col != -1 && board[row, col] == '\0')
            {
                button.Text = currentPlayer.ToString();
                board[row, col] = currentPlayer;

                if (CheckWin(currentPlayer))
                {
                    MessageBox.Show($"Игрок {currentPlayer} победил!");
                    ClearBoard();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Ничья!");
                    ClearBoard();
                }
                else
                {
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                    BotMove();
                }
            }
        }

        private void BotMove()
        {
            Random random = new Random();

            int row, col;
            do
            {
                row = random.Next(0, BoardSize);
                col = random.Next(0, BoardSize);
            } while (board[row, col] != '\0');

            buttons[row, col].Text = currentPlayer.ToString();
            board[row, col] = currentPlayer;

            if (CheckWin(currentPlayer))
            {
                MessageBox.Show($"Игрок {currentPlayer} победил!");
                ClearBoard();
            }
            else if (IsBoardFull())
            {
                MessageBox.Show("Ничья!");
                ClearBoard();
            }
            else
            {
                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            }
        }

        private bool CheckWin(char player)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;

                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                    return true;
            }

            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[2, 0] == player && board[1, 1] == player && board[0, 2] == player))
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (board[i, j] == '\0')
                        return false;
                }
            }

            return true;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    board[i, j] = '\0';
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = true;
                }
            }
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    buttons[i, j].Enabled = !IsBoardFull() && board[i, j] == '\0';
                }
            }
        }
    }
}




