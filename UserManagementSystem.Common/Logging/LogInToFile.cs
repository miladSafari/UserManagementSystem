using System;
using System.IO;
using System.Text;

namespace UserManagementSystem.Common.Logging
{
    public static class LogIntoFile
    {
        private static readonly object _lock = new();
        private static readonly string _logPath =
            Path.Combine(AppContext.BaseDirectory, "logs", "app.log");

        public static void LogIntoFileMethos(string message)
        {
            try
            {
                var dir = Path.GetDirectoryName(_logPath)!;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}";

                lock (_lock) 
                {
                    File.AppendAllText(_logPath, line);
                }
            }
            catch
            {
            }
        }
    }
}