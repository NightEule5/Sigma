// Copyright (c) 2019 Phillip Leere (NightEule5)
// Subject to MIT License (found in LICENSE.md).
using System;
using System.Collections.Generic;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Sigma
{
    /// <summary>
    /// This class compiles build scripts so they can be executed.
    /// </summary>
    public class ScriptCompiler
    {
        private string[] References = new string[]
        {
            "System.dll",
            "SigmaAPI.dll"
        };

        public FileInfo[] Files { get; private set; }

        public ScriptCompiler(params FileInfo[] files) => Files = files;

        public void Compile(FileInfo output)
        {
            CompilerParameters parameters = new CompilerParameters(References)
            {
                GenerateInMemory = true,
                GenerateExecutable = true
            };
        }
    }
}
