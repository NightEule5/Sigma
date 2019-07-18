using SigmaOld.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SigmaOld.Models
{
    /// <summary>
    /// An Object Model for an option in a build.sigma file.
    /// </summary>
    public class OptionModel
    {
        
        [JsonRequired]
        [ImpliedValue]
        public object Default { get; set; }
        public string Regex { get; set; } = ".*";
        public Type Type { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}
