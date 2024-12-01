# Timersky.Log    
### Yet another logger    

## Installation - [Nuget](https://www.nuget.org/packages/Timersky.Log)
```
dotnet add package Timersky.Log --version 1.0.1
```

## Usage
```csharp
using Timersky.Log;

namespace SomeNamespace;

public class Program
{
    private Logger _log = new();
    // or
    // private Logger _log = new("PathToLogDirectory");

    public void Main(string[] args)
    {
        _log.Info("Hello World!");
        _log.Warning("Hello World!");
        _log.Error("Hello World!");
    }
}
```
