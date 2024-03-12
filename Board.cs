using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Board
{
    private const int BoardSize = 10;
    public char[,] grid;
    private Ship[] ships;

    public Board()
    {
        grid = new char[BoardSize, BoardSize];
        ships = new Ship[]
        {
            new Ship(4),
            new Ship(3),
            new Ship(3),
            new Ship(2),
            new Ship(2),
            new Ship(2),
            new Ship(1),
            new Ship(1),
            new Ship(1),
            new Ship(1)
        };
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                grid[i, j] = '-';
            }
        }
    }

    public void ManualShipPlacement()
    {
        foreach (Ship ship in ships)
        {
            Console.WriteLine($"Place ship of size {ship.Size}");

            bool placed = false;
            while (!placed)
            {
                Display(true);
                Console.WriteLine("Enter starting coordinates (letter number):");
                string startInput = Console.ReadLine().ToUpper();
                if(startInput==null || startInput.Length<2)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong Input");
                    continue;
                }
                int[] startCoords = ConvertInputToCoordinates(startInput);

                Console.WriteLine("Enter direction (H for horizontal, V for vertical):");
                char directionInput = Console.ReadKey().KeyChar;
                bool horizontal;
                if (directionInput == 'H' || directionInput == 'h')
                {
                    horizontal = true;
                }
                else if(directionInput == 'V' || directionInput == 'v')
                {
                    horizontal = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nWrong direction, choose H for horizontal or V for vertical");
                    continue;
                }

                Console.Clear();
               
               

                if (CheckShipPlacement(startCoords[0], startCoords[1], ship.Size, horizontal))
                {
                    if (ship.TryPlaceShip(startCoords[0], startCoords[1], horizontal, this))
                    {
                        placed = true;
                        ship.segments[startCoords[0], startCoords[1]]=2;
                    }
                    else
                    {
                        Console.WriteLine("Invalid placement. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Ships cannot touch each other or get outside the board. Try again.");

                }
            }
        }
    }

    public bool ReceiveAttack(int[] coords, Board board)
    {
        int x = coords[0];
        int y = coords[1];

        foreach (Ship ship in ships)
        {
            if (ship.Hit(x, y, board))
            {
                grid[x, y] = 'X';
                return true;
            }
        }

        grid[x, y] = 'O';
        return false;
    }

    public void Display(bool forPlayer)
    {
        Console.WriteLine("\n");
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < BoardSize; i++)
        {

            if (i != 9) Console.Write(" " + (i + 1) + " ");
            else Console.Write("10 ");
            for (int j = 0; j < BoardSize; j++)
            {
                ConsoleColor color = GetCellColor(grid[i, j],forPlayer);
                Console.ForegroundColor = color;

                if (forPlayer)
                {
                    Console.Write(grid[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    if (grid[i, j] == '#')
                    {

                        Console.Write("- ");
                        Console.ResetColor();
                    }
                    else
                    {

                        Console.ForegroundColor = color;
                        Console.Write(grid[i, j] + " ");
                        Console.ResetColor();
                    }

                }

            }
            Console.WriteLine();
        }
    }

    public bool AllShipsSunk()
    {
        foreach (Ship ship in ships)
        {
            if (!ship.IsSunk())
                return false;
        }
        return true;
    }

    public bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    public bool IsVacant(int x, int y)
    {
        return grid[x, y] == '-';
    }

    public void PlaceShip(int x, int y)
    {
        grid[x, y] = '#';
    }

    public void RemoveShip(int x, int y)
    {
        grid[x, y] = '-';
    }

    public bool CheckShipPlacement(int x, int y, int size, bool horizontal)
    {
        if (horizontal)
        {
            for (int i = y; i < y + size; i++)
            {
                if (!IsValidCoordinate(x, i) || !IsVacant(x, i))
                    return false;
            }
            
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j < y + size + 1; j++)
                {
                    if (IsValidCoordinate(i, j) && !IsVacant(i, j))
                        return false;
                }
            }
        }
        else
        {
            for (int i = x; i < x + size; i++)
            {
                if (!IsValidCoordinate(i, y) || !IsVacant(i, y))
                    return false;
            }
           
            for (int i = x - 1; i < x + size + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (IsValidCoordinate(i, j) && !IsVacant(i, j))
                        return false;
                }
            }
        }
        return true;
    }

    public int[] ConvertInputToCoordinates(string input)
    {
        int y;
        try
        {
             y = int.Parse(input.Substring(1)) - 1;
        }
        catch 
        {
             y = -1;
        }
        int x = input[0] - 'A';
        
        return new int[] { y, x };
    }

    public ConsoleColor GetCellColor(char cell,bool forPlayer)
    {
        switch (cell)
        {
            case '-':
                return ConsoleColor.White;
            case '#':
                if(forPlayer)
                {
                    return ConsoleColor.Cyan;
                }
                return ConsoleColor.White;
            case 'X':
                return ConsoleColor.Red;
            case 'O':
                return ConsoleColor.Gray;
            case '~':
                return ConsoleColor.Yellow;
            default:
                return ConsoleColor.White;
        }
    }

}