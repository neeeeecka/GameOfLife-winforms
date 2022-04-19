using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{

    public class Grid
    {
        public int Width;
        public int Height;

        private int[,] grid;
        
        public Grid() { }

        public Grid(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;

            grid = new int[Width, Height];

            for(int column = 0; column < Width; column++)
            {
                for(int row = 0; row < Height; row++)
                {
                    grid[column, row] = 0;
                }
            }
        }

        public int GetCell(int x, int y)
        {
            if (!isvalid(x,y)) return 0;
            return grid[x, y];
        }

        public void SetCell(int x, int y, int value)
        {
            if (!isvalid(x, y)) return;
            grid[x, y] = value;
        }

        //sides
        public int W(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x - 1, y];
        }

        public int E(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x + 1, y];
        }

        public int N(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x, y - 1];
        }

        public int S(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x, y + 1];
        }

        //diagonals
        public int NW(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x - 1, y - 1];
        }

        public int NE(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x + 1, y - 1];
        }
        public int SW(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x - 1, y + 1];
        }
        public int SE(int x, int y)
        {
            if (!isvalid(x, y)) return 0;
            return grid[x + 1, y + 1];
        }



        //check if x and y are within bounds

        public bool isvalid(int x, int y)
        {
            if (x < 0 || x > Width - 1)
                return false;
            if (y < 0 || y > Height - 1)
                return false;

            if (x - 1 < 0 || x + 1 > Width - 1)
                return false;
            if(y - 1 < 0 || y + 1 > Height - 1)
                return false;

            return true;
        }

        //get number of alive neighbours
        public int AliveNeighbours(int x, int y)
        {
            int sum = 0;

            if (W(x, y) != 0)
                sum++;
            if (E(x, y) != 0)
                sum++;
            if (N(x, y) != 0)
                sum++;
            if (S(x, y) != 0)
                sum++;

            if (NW(x, y) != 0)
                sum++;
            if (NE(x, y) != 0)
                sum++;
            if (SW(x, y) != 0)
                sum++;
            if (SE(x, y) != 0)
                sum++;

            return sum;
        }

    }
}
