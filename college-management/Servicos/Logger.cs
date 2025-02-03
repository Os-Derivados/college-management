using System;
using System.IO;

namespace college_management.Utilitarios
{
    public class Logger
    {
        private readonly string _logDirectory;

        public Logger(string logDirectory = "logs")
        {
            _logDirectory = logDirectory;

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] - {message}";
            string logFilePath = Path.Combine(_logDirectory, "logfile.txt");

            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }

        public enum LogLevel
        {
            Info,
            Warn,
            Error
        }
    }
}
