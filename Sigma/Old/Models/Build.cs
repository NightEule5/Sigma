using Newtonsoft.Json;
using SigmaOld.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Models
{
    public class Build
    {
        public class Option
        {
            [JsonRequired]
            public object Default { get; set; }
            public string Regex { get; set; } = ".*";
            public Type Type { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
        }

        public class Binary
        {
            public string Name { get; set; }
            public Version Version { get; set; }
            public string FileName { get; set; }
            public List<LanguageModel> Languages { get; set; }
            public SourceInclusionModel Source { get; set; }
        }

        public class Language
        {
            public string Name { get; set; }
            public string Dialect { get; set; }
            public List<string> FileTypes { get; set; }
        }

        public Version Required { get; set; }
        public Dictionary<string, Option> Options { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public Dictionary<string, string> Defines { get; set; }
        public Dictionary<string, string[]> ExecArguments { get; set; }
    }
}
