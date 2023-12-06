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

    public void day2part1 (){
        string[] lines = Shared.ReadInFile("InputFiles/day2.txt");
        Dictionary<string,int> marbleDict = new Dictionary<string,int>{
            {"blue", 0}, 
            {"red", 0}, 
            {"green", 0}
        };

        var cleaned_line = "";
        string[] games;
        int roundNumber = 0;
        int answer = 0;
        bool flag = true;

        foreach (var line in lines){
            roundNumber += 1;
            flag = true;
            cleaned_line = Regex.Replace(line, @"^Game (1|[2-9]|[1-9][0-9]|100): ", "");
            games = cleaned_line.Split(";");
            foreach(var game in games){
                Match blueMatch = Regex.Match(game, @"(\d+) blue");
                Match redMatch = Regex.Match(game, @"(\d+) red");
                Match greenMatch = Regex.Match(game, @"(\d+) green");
                var reds = int.Parse(redMatch.Groups[1].Value == "" ? "0" : redMatch.Groups[1].Value);
                var blues = int.Parse(blueMatch.Groups[1].Value == "" ? "0" : blueMatch.Groups[1].Value);
                var greens = int.Parse(greenMatch.Groups[1].Value == "" ? "0" : greenMatch.Groups[1].Value);
                if(!(blues < 15 && reds < 13 && greens < 14)){
                    flag = false;
                }
            }
            if(flag){
                answer += roundNumber;
            }
        }
        Console.WriteLine(answer);
    }

    public void day2part2 (){
        string[] lines = Shared.ReadInFile("InputFiles/day2.txt");
        var cleaned_line = "";
        string[] games;
        int roundNumber = 0;
        int answer = 0;
        int max_reds = 1;
        int max_blues = 1;
        int max_greens = 1;

        foreach (var line in lines){
            roundNumber += 1;
            cleaned_line = Regex.Replace(line, @"^Game (1|[2-9]|[1-9][0-9]|100): ", "");
            games = cleaned_line.Split(";");
            max_reds = 1;
            max_blues = 1;
            max_greens = 1;
            foreach(var game in games){
                Match blueMatch = Regex.Match(game, @"(\d+) blue");
                Match redMatch = Regex.Match(game, @"(\d+) red");
                Match greenMatch = Regex.Match(game, @"(\d+) green");
                var reds = int.Parse(redMatch.Groups[1].Value == "" ? "0" : redMatch.Groups[1].Value);
                if(reds > max_reds){
                    max_reds = reds;
                }
                var blues = int.Parse(blueMatch.Groups[1].Value == "" ? "0" : blueMatch.Groups[1].Value);
                if(blues > max_blues){
                    max_blues = blues;
                }
                var greens = int.Parse(greenMatch.Groups[1].Value == "" ? "0" : greenMatch.Groups[1].Value);
                if(greens > max_greens){
                    max_greens = greens;
                }
            }
            answer += max_reds*max_blues*max_greens;
        }
        Console.WriteLine(answer);
    }

    public static (bool, int, int) IsValid(char[,] arr, int x, int y)
{
    int numRows = arr.GetLength(0);
    int numCols = arr.GetLength(1);

    // Check if the coordinates are within the bounds of the array
    if (x < 0 || x >= numRows || y < 0 || y >= numCols)
    {
        return (false, -1, -1);
    }

    // Check the eight surrounding points (including diagonals)
    for (int i = x - 1; i <= x + 1; i++)
    {
        for (int j = y - 1; j <= y + 1; j++)
        {
            // Skip the center point itself
            if (i == x && j == y)
            {
                continue;
            }

            // Check if the surrounding point is within the array bounds
            if (i >= 0 && i < numRows && j >= 0 && j < numCols)
            {
                // Check if the surrounding point is equal to '*'
                if (arr[i, j] == '*')
                {
                    return (true, i, j); // Return true if any surrounding point is '*'
                }
            }
        }
    }
        return (false, -1, -1); // Return false if none of the surrounding points are '*'
    }

    public void day3part1 (){

        string[] lines = Shared.ReadInFile("InputFiles/day3.txt");
        char[,] charArray = new char[lines.Count(), lines[0].Count()];
        char[,] validCharArray = new char[lines.Count(), lines[0].Count()];
        int i = 0;

        foreach (var line in lines){
            for(int j=0; j<line.Count(); j++){
                charArray[i,j]  =line.ElementAt(j);
                validCharArray[i,j] = '.';
            }
            i+=1;
        }

        for(int y=0; y<charArray.GetLength(1); y++){
            for(int x=0; x<charArray.GetLength(0); x++){
                if(Regex.IsMatch(charArray[x,y].ToString(), "[^.0-9]")){
                    charArray[x,y] = '*'; 
                }
            }
        }
        for(int iter=0; iter<5; iter++){
            for(int y=0; y<charArray.GetLength(1); y++){
                for(int x=0; x<charArray.GetLength(0); x++){
                    if(Regex.IsMatch(charArray[x,y].ToString(), "[0-9]")){
                        Match match = Regex.Match(lines[x], "[0-9]");
                        if(true){//IsValid(charArray, x, y)){
                            validCharArray[x,y] = charArray[x,y];
                        }
                        if(y-1 >= 0){
                            if(validCharArray[x, y-1] != '.'){
                                validCharArray[x,y] = charArray[x,y];
                            }
                        }
                        if(y+1 < charArray.GetLength(1)-1){
                            if(validCharArray[x, y+1] != '.'){
                                validCharArray[x,y] = charArray[x,y];
                            }
                        }
                    }
                }
            }   
        }

        List<int> answerList = new();

        for(int iter = 0; iter<validCharArray.GetLength(0); iter+=1){
            char[] rowArray = new char[validCharArray.GetLength(1)];
            for (int g = 0; g < validCharArray.GetLength(1); g++)
            {
                rowArray[g] = validCharArray[iter, g];
            }

            string word = new string(rowArray);
            string num = "";
            int index = 0;
            while(index < word.Count()){
                num = "";
                if(word.ElementAt(index) != '.'){
                    while(index < word.Count() && word.ElementAt(index)!= '.'){
                        num += word.ElementAt(index);
                        index += 1;
                    }
                    answerList.Add(int.Parse(num));
                }
                index += 1;            
            }
        }

        for(int y=0; y<validCharArray.GetLength(1); y++){
            for(int x=0; x<validCharArray.GetLength(0); x++){
                Console.Write(validCharArray[y,x]);
            }
            Console.WriteLine();
        }
        //Shared.PrintCollection<int>(answerList);
        Console.WriteLine(answerList.Sum());
    }

    struct tupleNumber {
        public (int,int) index;
        public char value;
    }

    public void day3part2 (){
         string[] lines = Shared.ReadInFile("InputFiles/day3.txt");
        char[,] charArray = new char[lines.Count(), lines[0].Count()];
        tupleNumber[,] validCharArray = new tupleNumber[lines.Count(), lines[0].Count()];
        int i = 0;

        foreach (var line in lines){
            for(int j=0; j<line.Count(); j++){
                charArray[i,j]  =line.ElementAt(j);
                validCharArray[i,j].value = '.';
                validCharArray[i,j].index = (-1,-1);
            }
            i+=1;
        }

        for(int y=0; y<charArray.GetLength(1); y++){
            for(int x=0; x<charArray.GetLength(0); x++){
                if(Regex.IsMatch(charArray[x,y].ToString(), "[^.0-9]")){
                    charArray[x,y] = '*'; 
                }
            }
        }
        for(int iter=0; iter<5; iter++){
            for(int y=0; y<charArray.GetLength(1); y++){
                for(int x=0; x<charArray.GetLength(0); x++){
                    if(Regex.IsMatch(charArray[x,y].ToString(), "[0-9]")){
                        Match match = Regex.Match(lines[x], "[0-9]");
                        if(IsValid(charArray, x, y).Item1){
                            validCharArray[x,y].value = charArray[x,y];
                            validCharArray[x,y].index = (IsValid(charArray, x, y).Item2, IsValid(charArray, x, y).Item3);
                        }
                        if(y-1 >= 0){
                            if(validCharArray[x, y-1].value != '.'){
                                validCharArray[x,y].value = charArray[x,y];
                                validCharArray[x,y].index = validCharArray[x, y-1].index;
                            }
                        }
                        if(y+1 < charArray.GetLength(1)-1){
                            if(validCharArray[x, y+1].value != '.'){
                                validCharArray[x,y].value = charArray[x,y];
                                validCharArray[x,y].index = validCharArray[x, y+1].index;
                            }
                        }
                    }
                }
            }   
        }

        List<int> answerList = new();
        List<(int,int)> gears = new();
        Dictionary<(int,int),int> tupleIntDic = new();
        string num = "";
        for(int iter = 0; iter<validCharArray.GetLength(0); iter+=1){
            int index = 0;
            while(index < validCharArray.GetLength(1)){
                num = "";
                if(validCharArray[iter,index].value != '.'){
                    while(index < validCharArray.GetLength(1) && validCharArray[iter,index].value!= '.'){
                        num += validCharArray[iter,index].value;
                        index += 1;
                    }
                    Console.WriteLine(num);
                    if(!tupleIntDic.ContainsKey(validCharArray[iter,index-1].index)){
                        tupleIntDic.Add(validCharArray[iter,index-1].index, int.Parse(num));
                    }
                    else{
                        tupleIntDic[validCharArray[iter,index-1].index] = tupleIntDic[validCharArray[iter,index-1].index] * int.Parse(num);
                        gears.Add(validCharArray[iter,index-1].index);
                    }
                }
                
                index += 1;            
            }
        }

        foreach(var kvp in tupleIntDic){
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        int sum = 0;

        foreach (var key in tupleIntDic.Keys)
        {
                if(gears.Contains(key)){
                    sum += tupleIntDic[key];
                }
        }

        Console.WriteLine(sum);
    }

    public void day4part1(){
        string[] lines = Shared.ReadInFile("InputFiles/day4.txt");
        
        double answer = 0;
        
        foreach (var line in lines){
            string[] leftList;
            string[] rightList;
            string tempLine = "";

            var colon = line.IndexOf(":")+2;
            tempLine = line.Substring(colon)
                        .Replace("  ", " ")
                        .Trim();
            var games = tempLine.Split("|");
            leftList = games[0].Trim().Split(" ");
            rightList = games[1].Trim().Split(" ");

            var intersect = leftList.Intersect(rightList);

            if(intersect.Count() > 0 ){
               answer +=  Math.Pow(2, intersect.Count()-1);
            }
        }
        Console.WriteLine(answer);
    }

    public void day4part2(){
        string[] lines = Shared.ReadInFile("InputFiles/day4.txt");
        Dictionary<int,int> intDic = new Dictionary<int, int>();

        for(int i=1; i<= lines.Length; i++){
            intDic[i] = 1;
        }
        
        int index = 1;

        foreach (var line in lines){
            string[] leftList;
            string[] rightList;
            string tempLine = "";

                var colon = line.IndexOf(":")+2;
                tempLine = line.Substring(colon)
                            .Replace("  ", " ")
                            .Trim();
                var games = tempLine.Split("|");
                leftList = games[0].Trim().Split(" ");
                rightList = games[1].Trim().Split(" ");

            var intersect = leftList.Intersect(rightList);

            for (int i=1; i<=intersect.Count(); i++){
                intDic[i+index]+=intDic[index];
            }
            index += 1;
        }

        foreach(var kvp in intDic){
            Console.WriteLine(kvp);
        }

        Console.WriteLine(intDic.Values.Sum());
    }

    }
}