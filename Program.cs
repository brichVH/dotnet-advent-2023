using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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

        public void day1part1 (){
                string[] lines = Shared.ReadInFile("InputFiles/test.txt");
                var count = 0;
                foreach (var word in lines){
                    string newString = Regex.Replace(word, "[^. 0-9]", "");
                    count += (int.Parse(newString.ElementAt(0).ToString() + newString.ElementAt(newString.Count()-1).ToString()));
                }

                Console.WriteLine(count);
            }

        public void day1part2 (){
                string[] lines = Shared.ReadInFile("InputFiles/day1.txt");
                List<int> intArr = new();
                var count = 0;
                Dictionary<int, string> numberDictionary = new Dictionary<int, string>
                    {
                        {1, "one"},
                        {2, "two"},
                        {3, "three"},
                        {4, "four"},
                        {5, "five"},
                        {6, "six"},
                        {7, "seven"},
                        {8, "eight"},
                        {9, "nine"}
                    };                
                
                foreach (var word in lines){
                    intArr = new();
                    char[] newString = word.ToCharArray();
                    for(int i=0; i<newString.Count(); i+=1){
                        if(Regex.IsMatch(newString[i].ToString(), "[0-9]")){
                            intArr.Add(int.Parse(newString[i].ToString()));
                        }
                        else{
                            foreach (var num in numberDictionary){
                                if(num.Value.Count() <= newString.Count()-i){
                                    if(word.Substring(i,num.Value.Count()) == num.Value){
                                        intArr.Add(num.Key);
                                    }
                                }
                            }
                        }
                }

                count += intArr[0]*10 + intArr[intArr.Count()-1];
            }
            Console.WriteLine(count);
    }
    class Program
    {
        static void Main()
        {
            SolutionsClass mySC = new();

            //mySC.day0part1();
            //mySC.day0part2();
            //mySC.day1part1();
            mySC.day1part2();
            
        }
    }
}
}