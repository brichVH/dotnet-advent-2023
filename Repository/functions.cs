using System;
using System.IO;
using IAdventOfCode;

namespace AdventOfCode
{
    public class Shared : IShared
    {
        public static String[] ReadInFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            return lines;
        }

        public static void PrintCollection<T>(IEnumerable<T> collection){
            foreach(T thing in collection){
                Console.WriteLine(thing);
            }
        }
    }
}