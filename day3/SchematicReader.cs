using System.Text.RegularExpressions;

static class SchematicReader{

    static string engineParts = Regex.Replace(File.ReadAllText(".\\res\\engine-schematic.txt"), @"\s|\t|\n|\r", "");
    static int length = new StreamReader(".\\res\\engine-schematic.txt").ReadLine().Length;

    public static void Run(){
        
        var Watch = System.Diagnostics.Stopwatch.StartNew();

        int sum = 0;
        int gearRatio;
        int gearRatioSum = 0;
        
        for (int i = 0; i < engineParts.Length; i++){

            if (!char.IsLetterOrDigit(engineParts[i]) && engineParts[i] != '.'){
                sum += DetectNumbers(i, out gearRatio);
                gearRatioSum += gearRatio;
            }
        }

        Watch.Stop();

        Console.WriteLine(sum);
        Console.WriteLine(gearRatioSum);
        Console.WriteLine($"Total Execution Time: {Watch.ElapsedMilliseconds} ms");
        
    }

    static int DetectNumbers(int pos, out int gearRatio){

        int sum = 0;
        gearRatio = (engineParts[pos] == '*') ? 1 : 0;
        
        //Not necessary because the input file doesn't contain any special characters on the edges
        bool[] sectors = new bool[]{true, true, true, true};

        if (pos%length == 0) sectors[0] = sectors[2] = false;
        if ((pos+1)%length == 0) sectors[1] = sectors[3] = false;
        if (pos-length < 0) sectors[0] = sectors[1] = false;
        if (pos+length > engineParts.Length) sectors[2] = sectors[3] = false;

        int i = 0;
        int counter = 0;
        int[] positions = PositionConverter(pos);

        foreach (bool b in sectors){
            if (b){
                for (; i < 12; i++){

                    string num = "0";
                    int numberIterator = positions[i];

                    if (char.IsDigit(engineParts[numberIterator])){
                        while (numberIterator%length != 0 && char.IsDigit(engineParts[numberIterator - 1])) numberIterator--;

                        while(char.IsDigit(engineParts[numberIterator])){
                        
                            num += engineParts[numberIterator];
                            engineParts = engineParts.Remove(numberIterator, 1).Insert(numberIterator, ".");
                            
                            if ((numberIterator+1)%length != 0) numberIterator++; else break;
                        }

                        counter++;
                    }

                    if (num != "0") gearRatio *= int.Parse(num); sum += int.Parse(num);

                }

            } else i += 3;
        }
        if (counter < 2) gearRatio = 0;
        return sum;
    }

    static int[] PositionConverter(int pos){
        return new int[]{pos-1, pos-length-1, pos-length, pos-length, pos-length+1, pos+1,
                         pos-1, pos+length-1, pos+length, pos+length, pos+length+1, pos+1};
    }
}