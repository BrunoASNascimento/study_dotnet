```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.413
  [Host]     : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean     | Error   | StdDev  |
|--------------- |---------:|--------:|--------:|
| SumWithForLoop | 438.6 ns | 1.68 ns | 1.40 ns |
| SumWithForEach | 322.9 ns | 0.55 ns | 0.43 ns |
| SumWithLinq    | 191.9 ns | 1.37 ns | 1.28 ns |
