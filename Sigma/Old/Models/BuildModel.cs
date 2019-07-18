using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Models
{
    /// <summary>
    /// An Object Model for a build.sigma file.
    /// </summary>
    public class BuildModel
    {
        public Version Required { get; set; }
        public Dictionary<string, OptionModel> Options { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public Dictionary<string, string> Defines { get; set; }
        public Dictionary<string, string[]> ExecArguments { get; set; }
    }
}
