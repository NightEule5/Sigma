using NLog;
using System;

namespace SigmaOld.OldBuildSystem.Stage
{
    public abstract class Stage
    {
        public abstract ILogger Log { get; set; }
        public abstract bool Run();

        /// <summary>
        /// Writes a stage progress string to the Console, but not the Log.
        /// This is done to make one line update throughout the stage, so
        /// as not to fill up the Console with spammed massages.
        /// TODO: Improve performance (maybe) by only reseting to a certain point (like to where the stage name is).
        /// </summary>
        /// <param name="value">The string to write.</param>
        public void WriteProgress(string value)
        {
            // Reset the line
            ResetLine();

            // Write the new progress
            Console.Write(value);
        }

        public void ResetLine()
        {
            // (https://stackoverflow.com/a/8946847)
            int cursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursor);
        }
    }
}
