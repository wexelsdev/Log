using System.Diagnostics;

namespace Timersky.Logger;

public sealed class Log
{
    private static string logDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}logs\\";
    private static string logFilePath;
    
    #region string

    public static void Info(string message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message, sender, LogType.Info, DateTime.UtcNow);
    }
        
    public static void Warning(string message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message, sender, LogType.Warning, DateTime.UtcNow);
    }
        
    public static void Error(string message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message, sender, LogType.Error, DateTime.UtcNow);
    }

    #endregion
    
    #region object

    public static void Info(object message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message.ToString(), sender, LogType.Info, DateTime.UtcNow);
    }
        
    public static void Warning(object message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message.ToString(), sender, LogType.Warning, DateTime.UtcNow);
    }
        
    public static void Error(object message)
    {
        StackFrame stackFrame = new(1);
        string sender = $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
            
        Send(message.ToString(), sender, LogType.Error, DateTime.UtcNow);
    }

    #endregion
    
    internal static Dictionary<LogType, string> LogNames = new()
    {
        { LogType.Info,    "INFORMATION" },
        { LogType.Warning, "  WARNING  " },
        { LogType.Error,   "   ERROR   " }
    };

    public static void LoadLogger()
    {
        logFilePath = $"{logDirPath}session-{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.log";
        
        if (!Directory.Exists(logDirPath))
        {
            Directory.CreateDirectory(logDirPath);
        }
        
        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }
    }
    
    internal static void Send(string message, string sender, LogType type, DateTime time)
    {
        using (FileStream file = new(logFilePath, FileMode.Append))
        {
            using (StreamWriter writer = new(file))
            {
                writer.WriteLine($"[{time:yyyy-MM-dd HH:mm:ss:ffff}] [{type}] [{sender}]: {message}");
            }
        }
        
        ConsoleColor backColor = Console.BackgroundColor;
        ConsoleColor foreColor = Console.ForegroundColor;
        ConsoleColor logColor = ConsoleColor.Magenta;

        switch (type)
        {
            case LogType.Info: logColor = ConsoleColor.Cyan; break;
            case LogType.Warning: logColor = ConsoleColor.Yellow; break;
            case LogType.Error: logColor = ConsoleColor.Red; break;
        }

        Console.ForegroundColor = backColor;
        Console.BackgroundColor = logColor;

        Console.Write($"{LogNames[type]}");
        
        Console.ForegroundColor = logColor;
        Console.BackgroundColor = backColor;
        
        Console.Write(" ");
        
        Console.ForegroundColor = backColor;
        Console.BackgroundColor = logColor;
        
        Console.Write($"{sender}");

        Console.ForegroundColor = logColor;
        Console.BackgroundColor = backColor;
        
        Console.Write(":");
        Console.Write($" {message}");
        Console.Write("\n");
        
        Console.ForegroundColor = foreColor;
    }
}
