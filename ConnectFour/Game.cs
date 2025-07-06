using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Game
    {
        public int[,] Matrica { get; set; }
        public int PlayerTurn { get; set; }
        public int First { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public String Winner { get; set; }

        public Game(string p1, string p2)
        {
            Matrica = new int[6, 7]; // 6X7 matrix for the game board
            First = 1; // Player 1 starts first
            PlayerTurn = First;
            Player1 = new Player(p1); // Initialize player 1
            Player2 = new Player(p2); // Initialize player 2
            Winner = "none";
        }
        public void ResetGame()
        {
            Matrica = new int[6, 7]; // Reset the game board
            First = First == 1 ? 2 : 1;
            PlayerTurn = First; // Switch first

        }
        public int AddScore(int column)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (Matrica[i, column] == 0) // Check for an empty cell
                {
                    Matrica[i, column] = PlayerTurn; // Place the player's token
                    if (CheckForWin(i, column))
                    {
                        if (PlayerTurn == 1) Winner = Player1.Name;
                        else Winner = Player2.Name;
                    }
                    PlayerTurn = PlayerTurn == 1 ? 2 : 1; // Switch turns between players
                    return i + 1;
                }
            }

            return 0;
        }

        public bool CheckForWin(int row, int col)
        {
            if (Matrica[row, col] == null)
                return false;

            int currentPlayer = Matrica[row, col];

            // Define directions: (deltaRow, deltaCol)
            var directions = new (int dRow, int dCol)[]
            {
                (0, 1),   // Horizontal
                (1, 0),   // Vertical
                (1, 1),   // Diagonal /
                (1, -1),  // Diagonal \
            };

            foreach (var (dRow, dCol) in directions)
            {
                int count = 1;

                // Check in the positive direction
                count += CountConsecutive(row, col, dRow, dCol, currentPlayer);
                // Check in the negative direction
                count += CountConsecutive(row, col, -dRow, -dCol, currentPlayer);

                if (count >= 4)
                    return true;
            }

            return false;
        }

        private int CountConsecutive(int startRow, int startCol, int dRow, int dCol, int player)
        {
            int count = 0;
            int row = startRow + dRow;
            int col = startCol + dCol;
            while (IsInsideBoard(row, col) && Matrica[row, col] == player)
            {
                count++;
                row += dRow;
                col += dCol;
            }
            return count;
        }
        private bool IsInsideBoard(int row, int col)
        {
            return row >= 0 && row < Matrica.GetLength(0) && col >= 0 && col < Matrica.GetLength(1);
        }

    }
}
