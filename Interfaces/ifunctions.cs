using System;
using System.IO;
using System.Net.NetworkInformation;

namespace IAdventOfCode
{
    public interface IShared
    {
        public abstract static String[] ReadInFile(string filename);
        public abstract static void PrintCollection<T>(IEnumerable<T> collection);
    }
}