```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                    | Mean       | Error     | StdDev    |
|-------------------------- |-----------:|----------:|----------:|
| SumWithLinq               |  10.058 μs | 0.1999 μs | 0.3112 μs |
| SumParallel               |  23.694 μs | 1.3162 μs | 3.8393 μs |
| SumWithForLoop            |   4.187 μs | 0.0647 μs | 0.0665 μs |
| SumWithForEach            |   3.546 μs | 0.0672 μs | 0.0525 μs |
| SumParallelFor            | 261.254 μs | 2.9509 μs | 2.7603 μs |
| SumParallelForWithLimit   | 226.319 μs | 4.3324 μs | 4.8155 μs |
| SumParallelForOptimized   |  22.723 μs | 0.3245 μs | 0.2877 μs |
| SumParallelForPartitioner |  24.656 μs | 1.6631 μs | 4.9036 μs |
