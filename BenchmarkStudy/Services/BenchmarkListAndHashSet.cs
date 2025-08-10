using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkStudy.Services;

public class BenchmarkListAndHashSet
{
    private int[] numbers;

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(1, 1000).ToArray();
    }

    [Benchmark]
    public int SumWithForLoopTwo()
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
            sum += numbers[i];
        return sum;
    }

    [Benchmark]
    public int SumWithForEachTwo()
    {
        int sum = 0;
        foreach (int i in numbers)
            sum += i;

        return sum;
    }

    [Benchmark]
    public int SumWithLinqTwo()
    {
        return numbers.Sum();
    }
}