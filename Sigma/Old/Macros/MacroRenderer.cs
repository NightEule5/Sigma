using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SigmaOld.Macros
{
    /// <summary>
    /// Renders macros in a Sigma file. Such macros begin with $ and
    /// are encapsulated with curly braces.
    /// </summary>
    public class MacroRenderer
    {
        private static Regex MacroRegex = new Regex(@"((?<!\\)\$[^ {}]+ )|((?<!\\)\${[^{}]+})");
        private static Regex VariableRegex = new Regex(@"[A-Za-z0-9-_]+(:([A-Za-z]+(\.[A-Za-z]+)*))?");
        private static Regex ConditionalRegex = new Regex(@"if\([A-Za-z0-9-_]\s*(==)|(!=)\s*(""[^""]"")|((true)|(false))|([0-9]+)\)");
        /// <summary>
        /// Checks whether a string can be rendered in the specified type.
        /// TODO: Perform checks here like whether variables exist and whether their type corisponds to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to render to.</typeparam>
        /// <param name="value">The value to render.</param>
        /// <returns>True if the value can be rendered to the specified type, false otherwise.</returns>
        public static bool CanRender<T>(string value) => true;

        public static bool CanRender(string value) => CanRender<string>(value);

        public static void Hello(string value)
        {
            
        }
    }
}
