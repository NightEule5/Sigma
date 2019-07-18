using System.Collections.Generic;

namespace SigmaOld.Macros
{
    public abstract class MacroFormat
    {
        /// <summary>
        /// The human-readable name of the format.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Checks whether the string is valid for this format. For example, a
        /// conditional string wouldn't be valid for a variable format.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string is valid for this format, false otherwise.
        /// </returns>
        public abstract bool IsValid(string value);

        /// <summary>
        /// Renders the format to an object.
        /// </summary>
        /// <param name="value">The format to render.</param>
        /// <returns>The rendered object, or null if no object would match.</returns>
        public abstract object Render(string value, Dictionary<string, object> variables);
    }
}
