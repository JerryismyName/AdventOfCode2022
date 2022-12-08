using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
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

        private static string[] GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static int[,] MapLines(string[] lines)
        {
            var map = new int[lines.Max(l => l.Length), lines.Length];

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map[x, y] = int.Parse(lines[y].Substring(x, 1));
                }
            }

            return map;
        }

        private static bool IsVisible(int x, int y, int[,] map)
        {
            var i = 1;
            var treeHeight = map[x, y];
            var cover = new bool[4];
            
            while (true)
            {
                if ((!cover[0] && x - i < 0) || (!cover[1] && y - i < 0) || (!cover[2] && x + i >= map.GetLength(0)) || (!cover[3] && y + i >= map.GetLength(1)))
                {
                    return true;
                }

                if (!cover[0] && map[x - i, y] >= treeHeight)
                {
                    cover[0] = true;
                }
                if (!cover[1] && map[x, y - i] >= treeHeight)
                {
                    cover[1] = true;
                }
                if (!cover[2] && map[x + i, y] >= treeHeight)
                {
                    cover[2] = true;
                }
                if (!cover[3] && map[x, y + i] >= treeHeight)
                {
                    cover[3] = true;
                }

                if (cover.All(b => b))
                {
                    return false;
                }

                i++;
            }
        }

        private static void Part1()
        {
            var lines = GetInput();
            var map = MapLines(lines);
            var visibleTrees = (2 * lines.Length) + (2 * lines.Max(l => l.Length)) - 4;

            for (var x = 1; x < lines.Max(l => l.Length) - 1; x++)
            {
                for (var y = 1; y < lines.Length - 1; y++)
                {
                    if (IsVisible(x, y, map))
                    {
                        visibleTrees++;
                    }
                }
            }

            Console.WriteLine($"Answer: {visibleTrees}");
        }

        private static int GetScenicScore(int x, int y, int[,] map)
        {
            var i = 1;
            var treeHeight = map[x, y];
            var cover = new bool[4];
            var trees = new int[4];

            while (true)
            {
                if (!cover[0])
                {
                    if (x - i < 0)
                    {
                        cover[0] = true;
                    }
                    else
                    {
                        if (map[x - i, y] >= treeHeight)
                        {
                            cover[0] = true;
                        }
                        trees[0] += 1;
                    }
                }
                if (!cover[1])
                {
                    if (y - i < 0)
                    {
                        cover[1] = true;
                    }
                    else
                    {
                        if (map[x, y - i] >= treeHeight)
                        {
                            cover[1] = true;
                        }
                        trees[1] += 1;
                    }
                }
                if (!cover[2])
                {
                    if (x + i > map.GetLength(0) - 1)
                    {
                        cover[2] = true;
                    }
                    else
                    {
                        if (map[x + i, y] >= treeHeight)
                        {
                            cover[2] = true;
                        }
                        trees[2] += 1;
                    }
                }
                if (!cover[3])
                {
                    if (y + i > map.GetLength(1) - 1)
                    {
                        cover[3] = true;
                    }
                    else
                    {
                        if (map[x, y + i] >= treeHeight)
                        {
                            cover[3] = true;
                        }
                        trees[3] += 1;
                    }
                }

                if (cover.All(t => t))
                {
                    return trees.Aggregate(1, (m, z) => m * z);
                }

                i++;
            }
        }

        private static void Part2()
        {
            var lines = GetInput();
            var map = MapLines(lines);
            var bestTreeScore = 0;

            for (var x = 1; x < lines.Max(l => l.Length) - 1; x++)
            {
                for (var y = 1; y < lines.Length - 1; y++)
                {
                    bestTreeScore = Math.Max(bestTreeScore, GetScenicScore(x, y, map));
                }
            }

            Console.WriteLine($"Answer: {bestTreeScore}");
        }
    }
}
