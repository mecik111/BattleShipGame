using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class Player
{
    public Board Board { get; }
    public Board EnemyBoard { get; }

    public int playerWins = 0;
   

    public Player()
    {
        Board = new Board();
    }

    public void SetupBoard()
    {
        Board.ManualShipPlacement();
    }

    public void Attack(Player opponent)
    {
        opponent.Board.Display(false);
        Console.WriteLine("Enter coordinates to attack:");
        string input = Console.ReadLine().ToUpper();
        int[] coords = Board.ConvertInputToCoordinates(input);
        Console.Clear();
        if(opponent.Board.grid[coords[0], coords[1]]=='O' || opponent.Board.grid[coords[0], coords[1]] == 'X')
        {
            Console.WriteLine("You have already shot at this place");
            Attack(opponent);
            return;
        }
        bool result = opponent.Board.ReceiveAttack(coords,opponent.Board);
        if (result)
        {
            Console.WriteLine("Hit!");
            if(opponent.AllShipsSunk())
            {
                return;
            }
            Attack(opponent);
            return;
        }
            
        else
            Console.WriteLine("Miss!");

        Console.WriteLine("\nOpponent's Board:");
        opponent.Board.Display(false);
    }

    public bool AllShipsSunk()
    {
        return Board.AllShipsSunk();
    }
}