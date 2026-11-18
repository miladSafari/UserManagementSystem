using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementSystem.Common.Logging
{
    public static class LogIntoFile
    {
        public static void LogIntoFileMethos(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("log something");

            File.AppendAllText(Environment.SystemDirectory + "log.txt", sb.ToString());
            sb.Clear();
        }

    }
}
