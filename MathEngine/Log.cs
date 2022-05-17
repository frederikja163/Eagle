using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace MathEngine
{
    public enum MessageSeverity : byte
    {
        Trace,
        Info,
        Warning,
        Error
    }
    
    public readonly struct MessageInfo
    {
        public readonly MessageSeverity Severity;
        public readonly DateTime Time;
        public readonly string Message;
        public readonly int LineNumber;
        public readonly string FilePath;

        public MessageInfo(MessageSeverity severity, DateTime time, string message, int lineNumber, string filePath)
        {
            Severity = severity;
            Time = time;
            Message = message;
            LineNumber = lineNumber;
            FilePath = Path.GetFileName(filePath);
        }

        public override string ToString()
        {
            return $"[{Severity} {Time.ToString("yyyy-MM-dd hh:mm:ss:fffff")} {FilePath}#{LineNumber}] {Message}";
        }
    }
    
    public interface ILogger
    {
        void Log(MessageInfo info);
    }
    
    public sealed class FileLogger : ILogger
    {
        private string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void Log(MessageInfo info)
        {
            using var sw = File.OpenWrite(_path);
            byte[] data = Encoding.UTF8.GetBytes(info.ToString());
            sw.Write(data);
            sw.WriteByte((byte)'\n');
        }
    }
    
    public sealed class ConsoleLogger : ILogger
    {
        public void Log(MessageInfo info)
        {
            Console.ForegroundColor = SeverityToColor(info.Severity);
            Console.WriteLine(info.ToString());
        }
        
        private static ConsoleColor SeverityToColor(MessageSeverity severity) =>
            severity switch
            {
                MessageSeverity.Trace => ConsoleColor.Gray,
                MessageSeverity.Info => ConsoleColor.White,
                MessageSeverity.Warning => ConsoleColor.Yellow,
                MessageSeverity.Error => ConsoleColor.Red,
                _ => throw new Exception("Could not convert severity to console color.")
            };
    }

    public static class Log
    {
        private static List<ILogger> _loggers = new List<ILogger>();

        public static void AddLogger(ILogger logger) => _loggers.Add(logger);

        public static void Trace(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            WriteLog(new MessageInfo(MessageSeverity.Trace, DateTime.Now, message, lineNumber, filePath));
        
        public static void Info(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            WriteLog(new MessageInfo(MessageSeverity.Info, DateTime.Now, message, lineNumber, filePath));
        
        public static void Warning(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            WriteLog(new MessageInfo(MessageSeverity.Warning, DateTime.Now, message, lineNumber, filePath));
        
        public static void Error(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") =>
            WriteLog(new MessageInfo(MessageSeverity.Error, DateTime.Now, message, lineNumber, filePath));

        private static void WriteLog(in MessageInfo info)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(info);
            }
        }
    }
}