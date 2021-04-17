### 兽音译者

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i9-10900KF CPU 3.70GHz, 1 CPU, 20 logical and 10 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  DefaultJob : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
```

|      Method |                aowua |     text |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |--------------------- |--------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
| **ConvertBack** | **~呜嗷呜呜(...)嗷~啊~啊 [68]** |        **?** | **144.33 ns** | **1.485 ns** | **1.389 ns** | **0.0038** |     **-** |     **-** |      **40 B** |
| **ConvertBack** | **~呜嗷呜呜(...)嗷~啊~啊 [36]** |        **?** |  **73.13 ns** | **0.831 ns** | **0.777 ns** | **0.0030** |     **-** |     **-** |      **32 B** |
|     **Convert** |                    **?** |     **嗷呜嗷呜** |  **54.68 ns** | **0.511 ns** | **0.478 ns** | **0.0092** |     **-** |     **-** |      **96 B** |
|     **Convert** |                    **?** | **嗷呜嗷呜嗷呜嗷呜** |  **98.45 ns** | **1.164 ns** | **0.972 ns** | **0.0153** |     **-** |     **-** |     **160 B** |
