using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        enum Rochambeau
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        enum Result
        {
            Lose = 0,
            Tie = 3, 
            Win = 6
        }

        static void Main(string[] args)
        {
            Console.Write(@"Press 1 or 2 for puzzle part: ");
            var result = Console.ReadKey();
            Console.WriteLine();

            if (result.KeyChar == '1')
            {
                Part1();
            }
            else if (result.KeyChar == '2')
            {
                Part2();
            }
            else
            {
                Console.WriteLine(@"Incorrect key press");
            }

            Console.WriteLine(@"Press any key to quit.");
            Console.ReadKey();
        }

        private static Rochambeau ConvertToRochambeau(char c)
        {
            switch (c)
            {
                case 'A':
                case 'X': return Rochambeau.Rock;
                case 'B':
                case 'Y': return Rochambeau.Paper;
                case 'C':
                case 'Z': return Rochambeau.Scissors;
                default: throw new ArgumentException($"Unknown character: {c}");
            }
        }

        private static Result ConvertToResult(char c)
        {
            switch (c)
            {
                case 'X': return Result.Lose;
                case 'Y': return Result.Tie;
                case 'Z': return Result.Win;
                default: throw new ArgumentException($"Unknown character: {c}");
            }
        }

        private static Result Outcome(Rochambeau opponentThrow, Rochambeau selfThrow)
        {
            if (opponentThrow == selfThrow)
            {
                return Result.Tie;
            }

            // Rock vs. scissors
            if (((int)opponentThrow + (int)selfThrow) == 4)
            {
                return opponentThrow < selfThrow ? Result.Lose : Result.Win;
            }

            return opponentThrow < selfThrow ? Result.Win : Result.Lose;
        }

        private static int Score1(Rochambeau opponentThrow, Rochambeau selfThrow)
        {
            return (int)selfThrow + (int)Outcome(opponentThrow, selfThrow);
        }

        private static string[] GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static int ProcessLine1(string line)
        {
            var split = line.Split(' ');
            return Score1(ConvertToRochambeau(split[0][0]), ConvertToRochambeau(split[1][0]));
        }

        private static void Part1()
        {
            var lines = GetInput();
            var score = 0;
            
            foreach (var line in lines)
            {
                score += ProcessLine1(line);
            }

            Console.WriteLine($"Final score: {score}");
        }

        private static Rochambeau WhatShouldIThrow(Rochambeau opponentThrow, Result desiredResult)
        {
            if (desiredResult == Result.Tie)
            {
                return opponentThrow;
            }

            if (desiredResult == Result.Win)
            {
                if (opponentThrow == Rochambeau.Scissors)
                {
                    return Rochambeau.Rock;
                }

                return (Rochambeau)((int)opponentThrow + 1);
            }

            if (opponentThrow == Rochambeau.Rock)
            {
                return Rochambeau.Scissors;
            }

            return (Rochambeau)((int)opponentThrow - 1);
        }

        private static int ProcessLine2(string line)
        {
            var split = line.Split(' ');
            var opponentThrow = ConvertToRochambeau(split[0][0]);
            var desiredResult = ConvertToResult(split[1][0]);
            var selfThrow = WhatShouldIThrow(opponentThrow, desiredResult);
            var result = (int)selfThrow + (int)desiredResult;
            return result;
        }

        private static void Part2()
        {
            var lines = GetInput();
            var score = 0;

            foreach (var line in lines)
            {
                score += ProcessLine2(line);
            }

            Console.WriteLine($"Final Score: {score}");
        }
    }
}
