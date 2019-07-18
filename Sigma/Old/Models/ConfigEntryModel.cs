using System.Collections.Generic;
using System.IO;

namespace SigmaOld.Models
{
    /// <summary>
    /// A class representing the Json contents of a config.sigma entry.
    /// </summary>
    public class ConfigEntryModel
    {
        public class Line
        {
            public int OnLine { get; set; }
            public string Replace { get; set; }
            public string With { get; set; }
        }

        public FileInfo File { get; set; }
        public Dictionary<string, Line> Lines { get; set; }
    }
}
