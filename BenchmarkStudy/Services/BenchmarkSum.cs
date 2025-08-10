using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkStudy.Services;

public class BenchmarkSum
{
    private int[] numbers;

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(1, 1000).ToArray();
    }

    [Benchmark]
    public int SumWithForLoop()
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
            sum += numbers[i];
        return sum;
    }

    [Benchmark]
    public int SumWithForEach()
    {
        int sum = 0;
        foreach (int i in numbers)
            sum += i;

        return sum;
    }

    [Benchmark]
    public int SumWithLinq()
    {
        return numbers.Sum();
    }
}