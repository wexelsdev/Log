namespace Timersky.Log;

[Serializable]
public class LoggerNotInitializedException : Exception
{
    public LoggerNotInitializedException() : base("Logger not initialized, use Initialize() before calling any method from logger.") { }
}
