using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Linq;

static class Game{

    static string[] games = File.ReadAllLines(".\\res\\game-results.txt");
    static string[] colours = new[]{"red", "green", "blue"};
    static int[] limits = new[]{12, 13, 14};
    
    public static void Run(){

        var watch = System.Diagnostics.Stopwatch.StartNew();

        int sum = 0;
        int powerSum = 0;

        for (int i = 0; i < 100; i++){
            int currentPower;

            if (ParseLine(games[i], out currentPower)){
                sum += i+1;
                powerSum += currentPower;
            }
        }

        watch.Stop();

        Console.WriteLine(sum);
        Console.WriteLine(powerSum);
        Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
    }

    static bool ParseLine(string line, out int power){
        
        string[] rounds = ("," + Regex.Replace(Regex.Replace(line.Remove(0, line.IndexOf(':') + 1), @"\s", ""), ";", @"$&,")).ToLower().Split(';');
        int[] maxValues = new int[3];

        int currentPos;
        int currentNum;

        power = 0;

        foreach (string s in rounds){

            for (int i = 0; i < 3; i++){
                if (s.Contains(colours[i])){
                    currentPos = s.LastIndexOf(",", s.IndexOf(colours[i])) + 1;

                    if (currentPos == s.IndexOf(colours[i])-1) currentNum = s[currentPos] - '0';
                    else currentNum = int.Parse(s.Substring(currentPos, 2));

                    if (currentNum > maxValues[i]) maxValues[i] = currentNum;

                    //Comment out for task 2 to be successful cuz it's the sum of all games for some godforsaken reason
                    if (currentNum > limits[i]) return false;
                }
            }
        }

        power = maxValues.Aggregate(1, (a, b) => a*b);

        return true;
    }
}