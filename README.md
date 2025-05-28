# Timersky.Log    
### Yet another logger    

## Attention!
### 🚚 Project has moved!
This repository is no longer maintained. The latest version of the project is now available on GitLab:
👉 [Click!](https://gitlab.com/wexelsdev/Log)

## Installation - [Nuget](https://www.nuget.org/packages/Timersky.Log)
```
dotnet add package Timersky.Log --version 1.0.9
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
