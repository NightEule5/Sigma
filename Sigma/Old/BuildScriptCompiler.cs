using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;

namespace SigmaOld
{
    public class BuildScriptCompiler
    {
        public FileInfo Source { get; private set; }
        public FileInfo Binary { get; private set; }

        public BuildScriptCompiler(FileInfo source, FileInfo binary)
        {
            Source = source;
            Binary = binary;
        }

        public bool Compile()
        {
            CSharpCompilation
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters()
            {
                OutputAssembly      = Binary.FullName,
                GenerateInMemory    = false,
                GenerateExecutable  = false
            };

            CompilerResults results = provider.CompileAssemblyFromFile(parameters, Source.FullName);

            if (results.Errors.HasErrors)
            {
                results.
            }

            return true;
        }
    }
}
