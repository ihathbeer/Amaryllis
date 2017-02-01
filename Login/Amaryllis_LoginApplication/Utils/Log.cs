using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amaryllis.Utils
{
    static class Log
    {
        public enum LogLevel
        {
            None = 0,
            Debug = 1,
            Error = 2,
            Warning = 4,
            Info = 8,
            Trace = 16,
            All = 31,
        }

        public static void Send(LogLevel Level, ConsoleColor Color, String Id, String Svc, String Msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            Console.ForegroundColor = Color;
            Console.Write(" " + Id + " ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Svc + ": ");

            Console.ForegroundColor = Color;
            Console.WriteLine(Msg);

            Console.ForegroundColor = ConsoleColor.Gray;

            String FileLog = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            FileLog += " " + Id + " " + Svc + ": " + Msg;
        }

        public static void Debug(String Svc, String Msg)
        {
            Send(LogLevel.Debug, ConsoleColor.Cyan, "D", Svc, Msg);
        }

        public static void Error(String Svc, String Msg)
        {
            Send(LogLevel.Error, ConsoleColor.Red, "E", Svc, Msg);
        }

        public static void Warning(String Svc, String Msg)
        {
            Send(LogLevel.Warning, ConsoleColor.Yellow, "W", Svc, Msg);
        }

        public static void Info(String Svc, String Msg)
        {
            Send(LogLevel.Info, ConsoleColor.Green, "I", Svc, Msg);
        }

        public static void Trace(String Svc, String Msg)
        {
            Send(LogLevel.Trace, ConsoleColor.Gray, "T", Svc, Msg);
        }
    }
}
