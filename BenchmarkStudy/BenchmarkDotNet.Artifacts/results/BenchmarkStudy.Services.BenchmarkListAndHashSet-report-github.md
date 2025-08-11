```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4770/24H2/2024Update/HudsonValley)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method            | Mean      | Error    | StdDev   |
|------------------ |----------:|---------:|---------:|
| SumWithForLoopTwo | 378.07 ns | 7.262 ns | 9.443 ns |
| SumWithForEachTwo | 332.40 ns | 6.556 ns | 8.973 ns |
| SumWithLinqTwo    |  78.23 ns | 1.187 ns | 1.219 ns |
