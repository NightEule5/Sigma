using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SigmaOld.OldBuildSystem.Compilers
{
    public abstract class Compiler
    {
        /// <summary>
        /// Gets the executable name with no file extension ("clang", "msvc", etc).
        /// </summary>
        public abstract string ExeName { get; }

        /// <summary>
        /// Gets the extension of the compiled file.
        /// </summary>
        public abstract string OutputExtension { get; }

        /// <summary>
        /// Constructs a command line for the specified source and compiled files.
        /// </summary>
        /// <param name="source">The source code to compile.</param>
        /// <param name="compiled">The compiled file.</param>
        /// <returns>The command line (not counting the executable name).</returns>
        public abstract string GetCommandLine(FileInfo source, FileInfo compiled);
    }
}
