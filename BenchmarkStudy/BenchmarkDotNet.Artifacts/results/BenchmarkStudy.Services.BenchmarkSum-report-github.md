```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.413
  [Host]     : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean     | Error   | StdDev  | Median   |
|--------------- |---------:|--------:|--------:|---------:|
| SumWithForLoop | 439.8 ns | 1.63 ns | 1.44 ns | 439.1 ns |
| SumWithForEach | 322.6 ns | 1.07 ns | 0.90 ns | 322.2 ns |
| SumWithLinq    | 193.3 ns | 3.85 ns | 5.64 ns | 189.6 ns |
