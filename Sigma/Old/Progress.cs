using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld
{
    public class Progress
    {
        private List<string> Data = new List<string>();

        /// <summary>
        /// Gets a string representing the current progress line.
        /// </summary>
        public string Current
        {
            get
            {
                string value = Data.Count > 0 ? Data[0] : string.Empty;

                for (int i = 1; i < Data.Count; i++)
                    value += $" : {Data[i]}";

                return value;
            }
        }

        /// <summary>
        /// Gets the progress line's length.
        /// </summary>
        public int Length
        {
            get
            {
                int l = Data.Count > 0 ? Data[0].Length : 0;

                for (int i = 1; i < Data.Count; i++)
                    l += 3 + Data[i].Length;

                return l;
            }
        }

        /// <summary>
        /// Adds an entry to the Console.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(string value)
        {
            Data.Add(value);
            Console.Write(Data.Count > 1 ? $" : {value}" : value);
        }

        /// <summary>
        /// Removes the last entry from the Console.
        /// </summary>
        public void RemoveLast()
        {
            // Remove the last string in the list plus the 3 delimiting characters.
            int count = Data[Data.Count - 1].Length + 3;
            Data.RemoveAt(Data.Count - 1);
            Remove(count);
        }

        /// <summary>
        /// Removes "<see cref="count"/>" characters from the console.
        /// </summary>
        /// <param name="count">The number of chars to remove.</param>
        private void Remove(int count)
        {
            int cursor = Console.CursorLeft;
            Console.SetCursorPosition(cursor - count, Console.CursorTop);
            Console.Write(new string(' ', count));
            Console.SetCursorPosition(cursor, Console.CursorTop);
        }

        /// <summary>
        /// Resets the Console to what it was before adding progress.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < Data.Count; i++)
                RemoveLast();
        }

        /// <summary>
        /// Refreshes the progress line, writing the whole thing out again.
        /// </summary>
        public void Refresh() => Console.Write(Current);

        public void LogInfo(Logger log)
        {
            string current = Current;
            Remove(Length);
            log.Info(current);
            Console.Write(current);
        }

        public void LogWarn(Logger log)
        {
            string current = Current;
            Remove(Length);
            log.Warn(current);
            Console.Write(current);
        }

        public void LogError(Logger log)
        {
            string current = Current;
            Remove(Length);
            log.Error(current);
            Console.Write(current);
        }
    }
}
