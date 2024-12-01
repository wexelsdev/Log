using System.Diagnostics;

namespace Timersky.Logger;

public sealed class Log
{
    private static string? _logFilePath;
    
    public static void Info(string message) => Send(message, GetSender(), LogType.Info, DateTime.UtcNow);
    public static void Warning(string message) => Send(message, GetSender(), LogType.Warning, DateTime.UtcNow);
    public static void Error(string message) => Send(message, GetSender(), LogType.Error, DateTime.UtcNow);
    
    public static void Info(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Info, DateTime.UtcNow);
    public static void Warning(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Warning, DateTime.UtcNow);
    public static void Error(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Error, DateTime.UtcNow);
    
    public static void LoadLogger()
    {
        var logDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}logs\\";
        _logFilePath = $"{logDirPath}session-{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.log";
        
        if (!Directory.Exists(logDirPath))
        {
            Directory.CreateDirectory(logDirPath);
        }
        
        if (!File.Exists(_logFilePath))
        {
            File.Create(_logFilePath).Close();
        }
    }

    private static string GetSender()
    {
        StackFrame stackFrame = new(2);
        return $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
    } 
    
    private static readonly Dictionary<LogType, string> LogNames = new()
    {
        { LogType.Info,    "INFORMATION" },
        { LogType.Warning, "  WARNING  " },
        { LogType.Error,   "   ERROR   " }
    };
    
    private static void Send(string message, string sender, LogType type, DateTime time)
    {
        if (_logFilePath != null)
        {
            using FileStream file = new(_logFilePath, FileMode.Append);
            using StreamWriter writer = new(file);
            writer.WriteLine($"[{time:yyyy-MM-dd HH:mm:ss:ffff}] [{type}] [{sender}]: {message}");
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
