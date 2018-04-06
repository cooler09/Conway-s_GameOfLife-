using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static int gridHeight;
        static int gridWidth;
        static void Main(string[] args)
        {
            //beacon
            //blinker
            //pulsar
            bool[,] grid = Init("beacon");

            PrintGrid(grid);
            while (true)
            {
                //Calculate life and assign it back to the original grid
                grid = NextTick(grid);
                PrintGrid(grid);

                //This will pause the screen for half a second
                System.Threading.Thread.Sleep(500);
            }

        }
        static bool[,] Init(string type = "")
        {
            bool[,] grid;
            switch (type.ToLower())
            {
                case "blinker":
                    gridHeight = 5;
                    gridWidth = 5;
                    grid = new bool[gridWidth, gridHeight];
                    grid[1, 2] = true;
                    grid[2, 2] = true;
                    grid[3, 2] = true;
                    break;
                case "beacon":
                    gridHeight = 6;
                    gridWidth = 6;
                    grid = new bool[gridWidth, gridHeight];
                    grid[1, 1] = true;
                    grid[1, 2] = true;
                    grid[2, 1] = true;
                    grid[4, 3] = true;
                    grid[4, 4] = true;
                    grid[3, 4] = true;
                    break;
                case "pulsar":
                    gridHeight = 17;
                    gridWidth = 17;
                    grid = new bool[gridWidth, gridHeight];
                    grid[2, 10] = true;
                    grid[2, 11] = true;
                    grid[2, 12] = true;
                    grid[10, 2] = true;
                    grid[11, 2] = true;
                    grid[12, 2] = true;
                    grid[2, 4] = true;
                    grid[2, 5] = true;
                    grid[2, 6] = true;
                    grid[4, 2] = true;
                    grid[5, 2] = true;
                    grid[6, 2] = true;
                    break;
                default:
                    gridHeight = 4;
                    gridWidth = 4;
                    grid = new bool[gridWidth, gridHeight];
                    grid[1, 1] = true;
                    grid[1, 2] = true;
                    grid[2, 1] = true;
                    grid[2, 2] = true;
                    break;
            }
            return grid;
        }
        static void PrintGrid(bool[,] grid)
        {
            Console.Clear();
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if(grid[i, j])
                        Console.Write("X");
                    else
                        Console.Write("O");
                }
                Console.WriteLine();
            }

        }
        static bool[,] NextTick(bool[,] grid)
        {
            bool[,] newGrid = new bool[gridWidth,gridHeight];
            //Fill the new grid with the original state

            for(int i = 0; i < gridWidth; i++)
            {
                for(int j = 0; j <gridHeight; j++)
                {
                    //default the newgrid cell to the old grid state
                    newGrid[i, j] = grid[i, j];

                    //count all the neighbors
                    var neighbors = CountNeighbors(grid,i,j);
                    //if the cell is alive
                    if(grid[i,j])
                    {
                        //check for underpopulation
                        if (neighbors < 2)
                            newGrid[i, j] = false;
                        //check for overpopulation
                        if (neighbors>3)
                            newGrid[i, j] = false;
                    }
                    else
                    {
                        //check for birth
                        if(neighbors ==3)
                            newGrid[i, j] = true;
                    }
                }
            }
            return newGrid;
        }
        static void FillNewGrid(bool[,] original, bool[,] newGrid)
        {
            for(int i =0; i < gridWidth; i++)
            {
                for(int j = 0; j < gridHeight; j++)
                {
                    newGrid[i, j] = original[i, j];
                }
            }
        }
        static int CountNeighbors(bool[,] grid, int posX, int posY)
        {
            int neighbors = 0;
            if (!IsOutOfBounds(posX +1,posY) &&grid[posX + 1, posY])
            {
                //OOO
                //OOX
                //OOO
                neighbors++;
            }
            if (!IsOutOfBounds(posX + 1, posY + 1) && grid[posX + 1, posY + 1])
            {
                //OOO
                //OOO
                //OOX
                neighbors++;
            }
            if (!IsOutOfBounds(posX + 1, posY - 1) && grid[posX + 1, posY - 1])
            {
                //OOX
                //OOO
                //OOO
                neighbors++;
            }
            if (!IsOutOfBounds(posX - 1, posY) && grid[posX - 1, posY])
            {
                //OOO
                //XOO
                //OOO
                neighbors++;
            }
            if (!IsOutOfBounds(posX - 1, posY + 1) && grid[posX - 1, posY + 1])
            {
                //OOO
                //OOO
                //XOO
                neighbors++;
            }
            if (!IsOutOfBounds(posX - 1, posY - 1) && grid[posX - 1, posY - 1])
            {
                //XOO
                //OOO
                //OOO
                neighbors++;
            }
            if (!IsOutOfBounds(posX, posY + 1) && grid[posX, posY + 1])
            {
                //OOO
                //OOO
                //OXO
                neighbors++;
            }
            if (!IsOutOfBounds(posX, posY-1) && grid[posX, posY - 1])
            {
                //OXO
                //OOO
                //OOO
                neighbors++;
            }
            return neighbors;
        }
        static bool IsOutOfBounds(int xPos, int yPos)
        {
            bool outOfBounds = false;
            if (xPos < 0)
                outOfBounds = true;
            if(xPos > gridWidth-1)
                outOfBounds = true;
            if(yPos < 0)
                outOfBounds = true;
            if (yPos > gridHeight - 1)
                outOfBounds = true;

            return outOfBounds;
        }
    }
}
