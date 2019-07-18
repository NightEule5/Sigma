using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigmaOld
{
    /// <summary>
    /// Acts as the container for parameters passed to Sigma via the command-line.
    /// </summary>
    public static class Parameters
    {
        // Cache

        public static bool CreateCache = true;
        public static DirectoryInfo CacheDir = new DirectoryInfo(Path.Combine(Program.DataPath, "Cache"));
    }
}
