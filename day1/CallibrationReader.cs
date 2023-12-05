using System.IO;

public static class CallibrationReader{

    private static string[] callibrationValues = File.ReadAllLines(".\\res\\callibration-values.txt");

    private static string[] numberDictionary = new string[10];

    public static void Run(){

        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        int sum = 0;

        FillArray();
        
        foreach (string s in callibrationValues){
            sum += Parser(s);
        }

        watch.Stop();

        Console.WriteLine(sum);
        Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

    }

    private static int Parser(string line){

        int[] values = new int[2];
        
        for (int x = 0; x < line.Length; x++){
            for (int y = 1; y < 10; y++){
                if (char.IsDigit(line[x]) && line[x] != '0' && y == int.Parse(line[x].ToString())){

                        if (values[0] == 0) values[0] = y; else values[1] = y;
                }
                if (x + numberDictionary[y].Length < line.Length){
                    if (string.Compare(line, x, numberDictionary[y], 0, numberDictionary[y].Length,
                            comparisonType: StringComparison.OrdinalIgnoreCase) == 0){

                            if (values[0] == 0) values[0] = y; else values[1] = y;
                            x += numberDictionary[y].Length-2;
                    }
                }
            }
        }

        if (values[1] == 0) {values[1] = values[0];}

        return int.Parse(values[0].ToString() + values[1].ToString());
    }

    private static void FillArray(){
        
        numberDictionary[1] = "one";
        numberDictionary[2] = "two";
        numberDictionary[3] = "three";
        numberDictionary[4] = "four";
        numberDictionary[5] = "five";
        numberDictionary[6] = "six";
        numberDictionary[7] = "seven";
        numberDictionary[8] = "eight";
        numberDictionary[9] = "nine";
    }
}