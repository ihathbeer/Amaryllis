namespace Amaryllis.Utils {
    static class Log {
        public enum LogLevel {
            None = 0,
            Debug = 1,
            Error = 2,
            Warning = 4,
            Info = 8,
            Trace = 16,
            All = 31,
        }

        public static void Send(LogLevel Level, System.ConsoleColor Color, string Id, string Svc, string Msg) {
            System.Console.ForegroundColor = System.ConsoleColor.Gray;
            System.Console.Write(System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString());
            System.Console.ForegroundColor = Color;
            System.Console.Write(" " + Id + " ");
            System.Console.ForegroundColor = System.ConsoleColor.White;
            System.Console.Write(Svc + ": ");
            System.Console.ForegroundColor = Color;
            System.Console.WriteLine(Msg);
            System.Console.ForegroundColor = System.ConsoleColor.Gray;
            string FileLog = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
            FileLog += " " + Id + " " + Svc + ": " + Msg;
        }

        public static void Debug(string Svc, string Msg) {
            Send(LogLevel.Debug, System.ConsoleColor.Cyan, "D", Svc, Msg);
        }

        public static void Error(string Svc, string Msg) {
            Send(LogLevel.Error, System.ConsoleColor.Red, "E", Svc, Msg);
        }

        public static void Warning(string Svc, string Msg) {
            Send(LogLevel.Warning, System.ConsoleColor.Yellow, "W", Svc, Msg);
        }

        public static void Info(string Svc, string Msg) {
            Send(LogLevel.Info, System.ConsoleColor.Green, "I", Svc, Msg);
        }

        public static void Trace(string Svc, string Msg) {
            Send(LogLevel.Trace, System.ConsoleColor.Gray, "T", Svc, Msg);
        }
    }
}
