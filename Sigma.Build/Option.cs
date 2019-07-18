// Copyright (c) 2019 Phillip Leere (NightEule5)
// Subject to MIT License (found in LICENSE.md).
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sigma
{
    public class Option
    {
        /*
         * if (typeof(T).IsPrimitive || typeof(T) != typeof(string))
         *      throw new NotSupportedException($"Type {typeof(T)} invalid for an Option. Must be a primitive type or a string.");
         */

        public object Default { get; private set; }
        public Type Type { get; private set; }
        public Regex Regex { get; set; }
        public string Description { get; set; }
        public Uri HelpLink { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Option"/> with a <see cref="string"/> value type.
        /// </summary>
        public Option()
        {
            Default = default(string);
            Type = typeof(string);
            Regex = new Regex(".*");
            Description = null;
            HelpLink = null;
        }

        /// <summary>
        /// Creates a new <see cref="Option"/> with the specified default value and type.
        /// </summary>
        /// <typeparam name="T">The option type, inferred from usage.</typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Option Create<T>(T defaultValue) => new Option()
        {
            Default = defaultValue, Type = typeof(T)
        };
    }
}
