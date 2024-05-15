using System;

class Player
{
    public Board OwnBoard { get; }
    public Board ShotBoard { get; } 

    public int playerWins = 0;

    public Player()
    {
        OwnBoard = new Board();
        ShotBoard = new Board();
    }

    public void SetupBoard()
    {
        OwnBoard.ManualShipPlacement();
        OwnBoard.Display(true);
    }

    public void Attack(Player opponent)
    {
        Console.WriteLine("Your Board:");
        OwnBoard.Display(true);
        Console.WriteLine("\nOpponent's Board:");
        ShotBoard.Display(false);
        Console.WriteLine("Enter coordinates to attack:");
        string input = Console.ReadLine().ToUpper();
        if (input == null || input.Length < 2)
        {
            Console.Clear();
            Console.WriteLine("Wrong Input");
            Attack(opponent);
            return;
        }

        int[] coords = OwnBoard.ConvertInputToCoordinates(input);
        if (coords[0] < 0 || coords[0] > 9 || coords[1] < 0 || coords[1] > 9)
        {
            Console.Clear();
            Console.WriteLine("Wrong Input");
            Attack(opponent);
            return;
        }

        Console.Clear();

     

        if (opponent.OwnBoard.grid[coords[0], coords[1]] == 'O' || opponent.OwnBoard.grid[coords[0], coords[1]] == 'X')
        {
            Console.WriteLine("You have already shot at this place");
            Attack(opponent);
            return;
        }

        bool result = opponent.OwnBoard.ReceiveAttack(coords, opponent.OwnBoard, ShotBoard);
        if (result)
        {
            Console.WriteLine("Hit!");

            if (opponent.AllShipsSunk())
                return;

            ShotBoard.grid[coords[0], coords[1]] = 'X';
           
        }
        else
        {
            Console.WriteLine("Miss!");
            ShotBoard.grid[coords[0], coords[1]] = 'O'; 
        }

       

        if (result)
        {
            Console.Clear();
            Console.WriteLine("Hit!, You have another turn");
            Console.ReadLine();
            Attack(opponent);
        }
    }


    public bool AllShipsSunk()
    {
        return OwnBoard.AllShipsSunk();
    }
}
