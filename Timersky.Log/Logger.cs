using System.Diagnostics;

namespace Timersky.Log;

public sealed class Logger
{
    private static string? _logFilePath;

    public Logger(string logDirPath = "")
    {
        if (string.IsNullOrEmpty(logDirPath))
        {
            logDirPath = $"{AppDomain.CurrentDomain.BaseDirectory}logs\\";
        }
        
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
    
    public void Info(string message) => Send(message, GetSender(), LogType.Info, DateTime.UtcNow);
    public void Warning(string message) => Send(message, GetSender(), LogType.Warning, DateTime.UtcNow);
    public void Error(string message) => Send(message, GetSender(), LogType.Error, DateTime.UtcNow);
    
    public void Info(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Info, DateTime.UtcNow);
    public void Warning(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Warning, DateTime.UtcNow);
    public void Error(object message) => Send(message.ToString() ?? string.Empty, GetSender(), LogType.Error, DateTime.UtcNow);

    private static string GetSender()
    {
        StackFrame stackFrame = new(2);
        return $"{stackFrame.GetMethod()!.DeclaringType}.{stackFrame.GetMethod()!.Name}";
    } 
    
    private readonly Dictionary<LogType, string> _logNames = new()
    {
        { LogType.Info,    "INFORMATION" },
        { LogType.Warning, "  WARNING  " },
        { LogType.Error,   "   ERROR   " }
    };
    
    private void Send(string message, string sender, LogType type, DateTime time)
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

        Console.Write($"{_logNames[type]}");
        
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
