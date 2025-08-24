using BenchmarkDotNet.Attributes;

namespace BenchmarkStudy.Services;

public class BenchmarkSum
{
    private int[] numbers;

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(1, 10_000).ToArray();
    }

    [Benchmark]
    public long SumWithLinq()
    {
        return numbers.Sum();
    }

    [Benchmark]
    public long SumWithLinqLong()
    {
        return numbers.Sum(x => (long)x);
    }


    [Benchmark]
    public long SumParallel()
    {
        return numbers.AsParallel().Sum(x => (long)x);
    }

    [Benchmark]
    public long SumWithForLoop()
    {
        long sum = 0;
        for (long i = 0; i < numbers.Length; i++)
            sum += numbers[i];
        return sum;
    }

    [Benchmark]
    public long SumWithForEach()
    {
        long sum = 0;
        foreach (long i in numbers)
            sum += i;

        return sum;
    }

    [Benchmark]
    public long SumParallelFor()
    {
        long sum = 0;
        Parallel.For(0, numbers.Length, i =>
        {
            Interlocked.Add(ref sum, numbers[i]);
        });
        return sum;
    }

    [Benchmark]
    public long SumParallelForWithLimit()
    {
        long sum = 0;
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
        Parallel.For(0, numbers.Length, i =>
        {
            Interlocked.Add(ref sum, numbers[i]);
        });
        return sum;
    }

    [Benchmark]
    public long SumParallelForOptimized()
    {
        object lockObj = new object();
        long sum = 0;

        Parallel.For(0, numbers.Length, () => 0, // Thread-local initializer
            (i, loop, localSum) =>
            {
                return localSum + numbers[i]; // Accumulate locally
            },
            localSum =>
            {
                lock (lockObj)
                {
                    sum += localSum; // Combine results once per thread
                }
            });

        return sum;
    }

    [Benchmark]
    public long SumParallelForPartitioner()
    {
        return numbers.AsParallel()
                      .WithDegreeOfParallelism(Environment.ProcessorCount)
                      .Sum(x => (long)x);
    }


}