/*
 * Copyright 2019 Phillip Leere
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
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
        /// Initializes a new instance of <see cref="Option"/> with the specified default
        /// value and type.
        /// </summary>
        /// <param name="type">The type; must match <paramref name="defaultValue"/>'s type.</param>
        /// <param name="defaultValue">The default value; type must match <paramref name="type"/>.</param>
        public Option(Type type, object defaultValue) : this()
            => Default = (Type = type) == defaultValue.GetType() ? defaultValue
            : throw new ArgumentException("The type of defaultValue does not " +
                $"match the Type given ({type.Name}).", "defaultValue");

        public Option(bool value) : this(typeof(bool), value) { }
        public Option(byte value) : this(typeof(byte), value) { }
        public Option(char value) : this(typeof(char), value) { }
        public Option(decimal value) : this(typeof(decimal), value) { }
        public Option(double value) : this(typeof(double), value) { }
        public Option(float value) : this(typeof(float), value) { }
        public Option(int value) : this(typeof(int), value) { }
        public Option(long value) : this(typeof(long), value) { }
        public Option(sbyte value) : this(typeof(sbyte), value) { }
        public Option(short value) : this(typeof(short), value) { }
        public Option(string value) : this(typeof(string), value) { }
        public Option(uint value) : this(typeof(uint), value) { }
        public Option(ulong value) : this(typeof(ulong), value) { }
        public Option(ushort value) : this(typeof(ushort), value) { }

        /// <summary>
        /// Creates a new <see cref="Option"/> with the specified default value and type.
        /// </summary>
        /// <typeparam name="T">The option type, inferred from usage.</typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Option Create<T>(T defaultValue) => new Option()
        {
            Default = defaultValue,
            Type = typeof(T)
        };
    }
}
