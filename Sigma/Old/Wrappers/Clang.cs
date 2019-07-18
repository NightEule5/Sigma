using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Wrappers
{
    /// <summary>
    /// A wrapper for the Clang/LLVM compiler ecosystem. Status: Incomplete
    /// </summary>
    public class Clang
    {
        /// <summary>
        /// The path to the directory where the Clang executable is located.
        /// </summary>
        public static string ClangExecutablePath { get; set; }

        public string SourceFile { get; set; }
        public string OutputFile { get; set; }
        public string ClStandard { get; set; } = "CL2.0";
        public string CppStandard { get; set; } = "c++17";
        public string CStandard { get; set; } = "c99";
        public string CudaArch { get; set; } = "sm_35";
        public int Optimize { get; set; } = 3;
        public string Warnings { get; set; } = "all";
        public Dictionary<string, object> Defines { get; private set; } = new Dictionary<string, object>();
        public List<string> FlagOverrides { get; private set; } = new List<string>();

        public string ConstructCommandLine()
        {
            
        }
    }
}
