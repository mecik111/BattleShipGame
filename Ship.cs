using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Ship
{
    private int size;
    private int hits;
    public int[,] segments;
    //0-empty, 1-shot, 2-ship

    public Ship(int size)
    {
        this.size = size;
      
        hits = 0;
        segments = new int[10,10];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                segments[i,j] = 0;
        }
    }

    public int Size { get { return size; } }

    public bool TryPlaceShip(int x, int y, bool horizontal, Board board)
    {
        if (!board.CheckShipPlacement(x, y, size, horizontal))
            return false;

        if (horizontal)
        {
            for (int i = y; i < y + size; i++)
            {
                board.PlaceShip(x, i);
                segments[x, i] = 2;
            }
        }
        else
        {
            for (int i = x; i < x + size; i++)
            {
                board.PlaceShip(i, y);
                segments[i, y] = 2;  
            }
        }

        return true;
    }

    public bool Hit(int x, int y, Board board)
    {
        if (segments[x,y]==0)
            return false;

        hits++;
        segments[x,y] = 1;
        if (IsSunk(board))
        {

        }
        return true;
    }

    public bool IsSunk(Board board=null)
    {
        if (board != null)
        {
            if (hits == size) BlockAround(board);

        }
        
        return hits == size;
    }

    public void BlockAround(Board board)
    {
        
        int[,] shipLocations = new int[size, 2];
        int partsFound = 0;
        for(int i  = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (segments[i,j]==1)
                {
                    shipLocations[partsFound,0]=i;
                    shipLocations[partsFound, 1] = j;
                    partsFound++;
                }
            }
        }
        for (int i = 0; i < size; i++)
        {
            bool left = false, right = false, up = false, down = false;
            if (!(shipLocations[i,1]+1>9))
            {
                down = true;
                board.grid[shipLocations[i, 0], shipLocations[i, 1] + 1] = '~';
            }

            if (!(shipLocations[i, 1] - 1 < 0 ))
            { 
                up = true;
                board.grid[shipLocations[i, 0], shipLocations[i, 1] - 1] = '~';
            }

            if (!(shipLocations[i, 0] + 1 > 9))
            {
                right = true;
                board.grid[shipLocations[i, 0] + 1, shipLocations[i, 1]] = '~';
            }

            if (!(shipLocations[i, 0] - 1 < 0))
            {
                left = true;
                board.grid[shipLocations[i, 0] - 1, shipLocations[i, 1]] = '~';
            }

            if(up&&right)
            {
                board.grid[shipLocations[i, 0] + 1, shipLocations[i, 1] - 1]  = '~';
            }

            if (up && left)
            {
                board.grid[shipLocations[i, 0] - 1, shipLocations[i, 1] - 1] = '~';
            }

            if (down && right)
            {
                board.grid[shipLocations[i, 0] + 1, shipLocations[i, 1] + 1] = '~';
            }
            if (down && left)
            {
                board.grid[shipLocations[i, 0] - 1, shipLocations[i, 1] + 1] = '~';
            }
        }
        for(int i = 0; i < size; i++)
        {
            board.grid[shipLocations[i, 0], shipLocations[i, 1]] = 'X';
        }
    }
}
