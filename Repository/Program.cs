using System.Collections;
using AdventOfCode;

namespace SolutionsNamespace {
    public class SolutionsClass {
        public void day0part1() {
            string[] lines = Shared.ReadInFile("InputFiles/day0.txt");
            var max = 0;
            var curr_total = 0;
            foreach(var word in lines) {
                if(word == "" && curr_total > max) {
                    max = curr_total;
                    curr_total = 0;
                }
                else if (word == "") {
                    curr_total = 0;
                }
                else {
                    curr_total += int.Parse(word);
                }
            }
            Console.WriteLine(max);
        }

        public void day0part2 (){
            string[] lines = Shared.ReadInFile("InputFiles/day0.txt");
            SortedSet<int> mySS = new();
            var curr_max = 0;
            foreach (var word in lines){
                if(word != ""){
                    curr_max += int.Parse(word);
                }
                else if(mySS.Count < 3){
                    mySS.Add(curr_max);
                    curr_max = 0;
                }
                else {
                    mySS.Add(curr_max);
                    curr_max = 0;
                    var removal = mySS.First();
                    mySS.Remove(removal);
                }

                //Shared.PrintCollection(mySS);
                //Console.WriteLine("\n");
            }

            Console.WriteLine(mySS.Sum());
            
        }
    }
    class Program
    {
        static void Main()
        {
            SolutionsClass mySC = new();

            mySC.day0part1();
            mySC.day0part2();
        }
    }
}