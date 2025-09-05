namespace BenchmarkStudy;

public class Program
{
    private static int _value = 0;

    public static void Main(string[] args)
    {
        //BenchmarkRunner.Run<BenchmarkSum>();
        //BenchmarkRunner.Run<BenchmarkListAndHashSet>();  
        int value = 0;
        for (int i = 0; i < 10; i++)
            Interlocked.Add(ref _value, 1);

        Console.WriteLine(value);

        for (int i = 0; i < 10; i++)
            Interlocked.Add(ref _value, 1);

        Console.WriteLine(value);

        //_value += value;
        //_value += value;
        //_value += value;
        Console.WriteLine(_value);
    }
}
