﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
      

        while (true)
        {
           
            Player player1 = new Player();
            Player player2 = new Player();

            
            Console.Clear();
            Console.WriteLine("Player 1's turn:");
            player1.SetupBoard();
            Console.Clear();
            Console.WriteLine("Player 2's turn:");
            player2.SetupBoard();


           
            while (!player1.AllShipsSunk() && !player2.AllShipsSunk())
            {
                Console.Clear();
                Console.WriteLine("Player 1's turn:");
                player1.Attack(player2);
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();

                if (player2.AllShipsSunk())
                    break;

                Console.Clear();
                Console.WriteLine("Player 2's turn:");
                player2.Attack(player1);
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

           
            Console.Clear();
            if (player1.AllShipsSunk())
            {
                Console.WriteLine("Player 2 wins!");
                player2.playerWins++;
            }
            else
            {
                Console.WriteLine("Player 1 wins!");
                player1.playerWins++;
            }

            Console.WriteLine($"\nPlayer 1 wins: {player1.playerWins}, Player 2 wins: {player2.playerWins}");
            Console.WriteLine("\nDo you want to play again? (Y/N)");
            string playAgainInput = Console.ReadLine();
            if (playAgainInput.ToUpper() != "Y")
                break;
        }
    }
}