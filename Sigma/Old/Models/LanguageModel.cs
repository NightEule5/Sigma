using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Models
{
    /// <summary>
    /// An Object Model for a programming Language.
    /// </summary>
    public class LanguageModel
    {
        public string Name { get; set; }
        public string Dialect { get; set; }
        public List<string> FileTypes { get; set; }
    }
}
