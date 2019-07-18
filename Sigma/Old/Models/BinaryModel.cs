using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Models
{
    /// <summary>
    /// An Object Model for a Binary in a build.sigma file.
    /// </summary>
    public class BinaryModel
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public string FileName { get; set; }
        public List<LanguageModel> Languages { get; set; }
        public SourceInclusionModel Source { get; set; }
    }
}
