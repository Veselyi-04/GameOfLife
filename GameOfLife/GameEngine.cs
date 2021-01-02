using System;

namespace GameOfLife
{
    class GameEngine
    {
        public uint CurrentGenerations { get; private set; }
        private bool[,] field; 

        private readonly int rows;
        private readonly int cols;

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;

            field = new bool[cols, rows];

            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(density) == 0;
                }
            }
        }

        public void nexGenerations()
        {
            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int Neighbourscount = countNeighbours(x, y);
                    bool isLife = field[x, y];

                    if (!isLife && Neighbourscount == 3)
                        newField[x, y] = true;
                    else if (isLife && (Neighbourscount < 2 || Neighbourscount > 3))
                        newField[x, y] = false;
                    else newField[x, y] = field[x, y];

                }
            }
            field = newField;
            CurrentGenerations++;
        }

        public bool[,] GetCurrentGenerations()
        {
            var result = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }

            return result;
        }

        private int countNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;

                    bool isSelfCheck = col == x && row == y;
                    bool isLife = field[col, row];

                    if (isLife && !isSelfCheck)
                        count++;

                }
            }
            return count;
        }

        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, state: true);
        }

        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, state: false);
        }

        private void UpdateCell(int x, int y, bool state)
        {
            if (x >= 0 && y >= 0 && x < cols && y < rows)
                field[x, y] = state;
        }
    }
}
