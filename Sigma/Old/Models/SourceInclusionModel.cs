using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Models
{
    /// <summary>
    /// An Object Model for source file inclusion and exclusion in a build.sigma file.
    /// </summary>
    public class SourceInclusionModel
    {
        public bool Recurse { get; set; } = true;
        public List<string> Include { get; set; }
        public List<string> Exclude { get; set; }
    }
}
