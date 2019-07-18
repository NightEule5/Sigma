using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaOld.Json
{
    /// <summary>
    /// An Attribute marking a value as implied. This is useful if, say,
    /// you want a variable to be set in an object when the user only
    /// gives a constant. For example: Setting OptionModel.Default to
    /// true when this json appears: <code>"Option": true</code>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ImpliedValueAttribute : Attribute { }
}
