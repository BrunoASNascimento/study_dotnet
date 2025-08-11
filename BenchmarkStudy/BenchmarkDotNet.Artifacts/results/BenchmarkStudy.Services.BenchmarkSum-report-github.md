```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4770/24H2/2024Update/HudsonValley)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method         | Mean      | Error    | StdDev   |
|--------------- |----------:|---------:|---------:|
| SumWithForLoop | 375.59 ns | 6.189 ns | 5.789 ns |
| SumWithForEach | 324.47 ns | 2.738 ns | 2.561 ns |
| SumWithLinq    |  78.27 ns | 0.679 ns | 0.635 ns |
