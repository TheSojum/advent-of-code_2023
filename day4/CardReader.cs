static class CardReader{

    static string[] lines = File.ReadAllLines(".\\res\\scratchcards.txt");

    static int[] cardCounter = new int[lines.Length];

    public static void Run(){

        var Watch = System.Diagnostics.Stopwatch.StartNew();

        int sum = 0;

        for (int i = 0; i < lines.Length; i++){

            sum += FindWinningNumbers(lines[i], i);
        }

        Watch.Stop();

        Console.WriteLine(sum);
        Console.WriteLine(cardCounter.Sum());
        Console.WriteLine($"Total Execution Time: {Watch.ElapsedMilliseconds} ms");
    }

    static int FindWinningNumbers(string line, int currentCard){

        line = line.Remove(0, line.IndexOf(':') + 1);

        int[] winningNumbers = Array.ConvertAll(line.Split('|')[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        int[] pulledNumbers = Array.ConvertAll(line.Split('|')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

        int matchingNumbers = pulledNumbers.Intersect(winningNumbers).Count();

        cardCounter[currentCard]++;
        for (int i = 1; i <= matchingNumbers; i++) cardCounter[currentCard + i] += cardCounter[currentCard];

        return (int) Math.Pow(2, matchingNumbers-1);
    }
}