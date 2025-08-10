```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.413
  [Host]     : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD


```
| Method            | Mean     | Error   | StdDev  |
|------------------ |---------:|--------:|--------:|
| SumWithForLoopTwo | 441.4 ns | 3.05 ns | 2.38 ns |
| SumWithForEachTwo | 323.2 ns | 0.36 ns | 0.28 ns |
| SumWithLinqTwo    | 190.4 ns | 1.83 ns | 1.43 ns |
