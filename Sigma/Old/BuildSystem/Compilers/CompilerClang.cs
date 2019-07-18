using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigmaOld.OldBuildSystem.Compilers
{
    public class CompilerClang : Compiler
    {
        public bool IsCpp { get; set; } = false;
        public bool IsCuda { get; set; } = false;
        public bool IsOpenCL { get; set; } = false;

        public override string ExeName => IsCpp ? "clang++" : "clang";

        public override string OutputExtension => ".o";

        public override string GetCommandLine(FileInfo source, FileInfo compiled)
        {
            return $"{source.FullName} -o {compiled.FullName}";
        }
    }
}
