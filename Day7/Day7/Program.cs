using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
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

        private static Fldr ParseStructure(string[] lines)
        {
            var structure = new Fldr("/", null);
            var currentFldr = structure;

            foreach (var line in lines)
            {
                if (line.StartsWith("$"))
                {
                    if (line.Substring(2).StartsWith("cd"))
                    {
                        if (line.Substring(5).Equals("/"))
                        {
                            currentFldr = structure;
                        }
                        else if (line.Substring(5).Equals("..") && currentFldr.Parent != null)
                        {
                            currentFldr = currentFldr.Parent;
                        }
                        else
                        {
                            var subfldr = currentFldr.SubFldrs.FirstOrDefault(f => f.Name.Equals(line.Substring(5)));
                            if (subfldr != null)
                            {
                                currentFldr = subfldr;
                            }
                        }
                    }
                }
                else
                {
                    if (line.StartsWith("dir"))
                    {
                        currentFldr.SubFldrs.Add(new Fldr(line.Substring(4), currentFldr));
                    }
                    else
                    {
                        var split = line.Split(' ');
                        currentFldr.Files[split[1]] = int.Parse(split[0]);
                    }
                }                
            }

            return structure;
        }

        private static void Part1()
        {
            var lines = GetInput();
            var structure = ParseStructure(lines);

            var under100K = structure.GetAllSubFldrs().Where(f => f.TotalSize < 100000);
            var count = under100K.Sum(f => f.TotalSize);
            
            if (structure.TotalSize < 100000)
            {
                count += structure.TotalSize;
            }

            Console.WriteLine($"Answer: {count}");
        }

        private static void Part2()
        {
            var lines = GetInput();
            var structure = ParseStructure(lines);
            var usedSpace = structure.FileSize + structure.GetAllSubFldrs().Select(s => s.FileSize).Sum();
            var freeSpace = 70000000 - usedSpace;
            var neededSpace = 30000000 - freeSpace;
            var overNeeded = structure.GetAllSubFldrs().Where(f => f.TotalSize >= neededSpace);
            var minOverNeeded = overNeeded.Min(f => f.TotalSize);

            Console.WriteLine($"Answer: {minOverNeeded}");
        }

        private class Fldr
        {
            public Fldr Parent { get; }
            public List<Fldr> SubFldrs { get; } = new List<Fldr>();
            public Dictionary<string, int> Files { get; } = new Dictionary<string, int>();
            public string Name { get; }
            public int FileSize => Files.Values.Sum();
            public int FolderSize => SubFldrs.Sum(s => s.TotalSize);
            public int TotalSize => FileSize + FolderSize;

            public Fldr(string name, Fldr parent)
            {
                Name = name;
                Parent = parent;
            }

            public IEnumerable<Fldr> GetAllSubFldrs()
            {
                var list = new List<Fldr>(SubFldrs);
                list.AddRange(SubFldrs.SelectMany(s => s.GetAllSubFldrs()));
                return list;
            }
        }
    }
}
