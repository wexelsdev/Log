# Timersky.Log    
### Yet another logger    

## Installation - [Nuget](https://www.nuget.org/packages/Timersky.Log)
```
dotnet add package Timersky.Log --version 1.0.8
```

## Usage
```csharp
using Timersky.Log;

namespace SomeNamespace;

public class Program
{    
    public static void Main(string[] args)
    {
        Log.Initialize();
        // or
        // Log.Initialize("PathToLogDirectory");

        Log.Info("Hello World!");
        Log.Warning("Hello World!");
        Log.Error("Hello World!");
        Log.Debug("Hello World!");
    }
}
```
