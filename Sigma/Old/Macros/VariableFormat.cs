using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SigmaOld.Macros
{
    public class VariableFormat : MacroFormat
    {
        /// <summary>
        /// This format's verification Regex. The Regex matches variable characters followed by an
        /// optional "type classifier", like this: <code>Hello:System.String</code>.
        /// </summary>
        private static Regex Regex = new Regex(@"[A-Za-z0-9_-]*(:([A-Za-z0-9_-]+(\.[A-Za-z0-9_-]+)*))?");
        public override string Name => "Variable";
        public override bool IsValid(string value) => Regex.IsMatch(value);
    }
}
