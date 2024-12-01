# Timersky.Logger
### Yet another logger

## Installation - [Nuget](https://www.nuget.org/packages/Timersky.Logger)
```
dotnet add package Timersky.Logger --version 1.0.0
```

## Usage
```csharp
using Timersky.Logger;

namespace SomeNamespace;

public class Program
{
    private Log _log = new();
    // or
    // private Log _log = new("PathToLogDirectory");

    public void Main(string[] args)
    {
        _log.Info("Hello World!");
        _log.Warning("Hello World!");
        _log.Error("Hello World!");
    }
}
```
