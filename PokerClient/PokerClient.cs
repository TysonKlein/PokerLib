using System;
using PokerLib;

namespace PokerClient
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.Write("Enter filename of input poker hands: ");
            string inputFileName = Console.ReadLine();

            PokerRound round = new PokerRound(inputFileName);

            round.DetermineWinner();
        }
    }
}
