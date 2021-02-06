using NameSorter.Shared.Abstractions;
using System;

namespace NameSorter.Shared.Implementations
{
    public class LoggingService : ILoggingService
    {
        public void LogError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}
