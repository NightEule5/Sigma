using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace SigmaOld
{
    public enum Command { Build, Gen, Convert }

    class Program
    {
        // TODO: Replace Console.WriteLine() calls with Log4Net calls.
        // TODO: Add custom stages and the ability to make extensions in C#.

        private static Logger Log = LogManager.GetLogger("Sigma");

        public static Version Version { get; } = "0.0.0";
        public static Dictionary<string, string> BinaryPaths = new Dictionary<string, string>();
        public static string DataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Sigma");
        public static string TempPath => Path.Combine(Path.GetTempPath(), "Sigma");

        public static Cache Cache { get; private set; } = new Cache();

        private static Command Command = default;
        private static Dictionary<string, string> CommandParameters = new Dictionary<string, string>()
        {
            { "Source", Environment.CurrentDirectory },
            { "Destination", Path.Combine(DataPath, "Build") }
        };

        public static bool Color { get; private set; } = true;

        public static string[] CommandLine { get; private set; }

        static void Main(string[] args)
        {
            CommandLine = args;
            ParseCommandLine();

            if (args.Length > 0)
            {
                // Parse the command.
                if (!Enum.TryParse(args[0], true, out Command))
                {
                    Log.Error($"{args[0]} not recognised as a command. See documentation for valid commands.");
                    return;
                }

                // Parse the arguments.
                for (int i = 1; i < args.Length; i++)
                {
                    string argument = args[i];
                    string value = i + 1 < args.Length ? args[i + 1] : null;

                    if (argument.StartsWith("--"))
                    {
                        string key = argument.Substring(2);

                        if (key.Equals("no-color"))
                            Color = false;
                        else if (key.Equals("cache") && value != null)
                            Cache.Init(new DirectoryInfo(value));
                    }
                    else if (argument.StartsWith("-"))
                    {
                        string key = argument.Substring(1);

                        if (key.Equals("no"))
                            Color = false;
                    }
                    else
                    {
                        // The command parameters
                    }
                }
            }
            else
            {
                Console.WriteLine("No command found. Put one in silly! XD");
                return;
            }
        }

        private static void ParseCommandLine()
        {
            if (CommandLine.Length > 0)
            {
                if (!Enum.TryParse(CommandLine[0], true, out Command))
                    Log.Warn($"Command \"{CommandLine[0]}\" not recognised, assuming {Command}.");

                // TODO: Parse the parameters
            }
            else
            {
                Log.Info($"Blank command line, assuming {Command}.");
                Command = default;
                CommandParameters = new Dictionary<string, string>()
                {
                    { "Source", Environment.CurrentDirectory },
                    { "Destination", Path.Combine(DataPath, "Build") }
                };
                return;
            }
        }
    }
}
