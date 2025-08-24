using BenchmarkDotNet.Running;
using BenchmarkStudy.Services;


namespace BenchmarkStudy;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<BenchmarkSum>();
        //BenchmarkRunner.Run<BenchmarkListAndHashSet>();  
    }
}
